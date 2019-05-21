using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PARRHI;
using PARRHI.Objects.State;
using FanucControllerLibrary;
using FanucControllerLibrary.DataTypes;
using PARRHI.Objects.Points;
using PARRHI.Objects;
using System.Linq;
using System;
using System.IO;
using UnityEditor;
using UnityEngine.UI;

public class ManualAttempt : MonoBehaviour
{
    Container Container;
    FanucController RobotController;

    UICanvas UICanvas;

    //Set by Unity editor
    public GameObject ARCamera;

    public GameObject UICanvasGO;
    public GameObject DevConsoleTextGameObject;
    private Text DevConsoleText;
    private Robot Robot;
    public GameObject RobotGO;
    public GameObject TaskObjects;
    public GameObject UITextCanvasObject;

    public bool ConnectEnabled;
    private bool ConnectionProcessStarted = false;
    private bool Connected;
    public bool SetDirection;
    public bool Animate;

    public double q1;
    public double q2;
    public double q3;
    public double q4;
    public double q5;
    public double q6;

    public bool random;
    public bool backToNull;
    public double q1t;
    public double q2t;
    public double q3t;
    public double q4t;
    public double q5t;
    public double q6t;


    private void Awake()
    {
        InstantiateSystem();
    }

    bool should_continue = true;
    // Update is called once per frame
    void Update()
    {

        if (ConnectEnabled && !ConnectionProcessStarted)
        {
            ConnectionProcessStarted = true;
            ConnectEnabled = false;
            InitRobotController();
        }

        UICanvasGO.transform.LookAt(-1 * ARCamera.transform.position);

        Cycle();

        if (should_continue)
            should_continue = PerformTaskStep();
    }

    /// <summary>
    /// Sets and creates references
    /// </summary>
    private void InstantiateSystem()
    {
        RobotController = new FanucController();

        //Set all output delegates
        PARRHI.Output.Instance.SetOutputDelegate(UnityOutputDelegate);
        PARRHI.Output.Instance.SetLogDelegate(UnityOutputDelegate);
        PARRHI.Output.Instance.SetErrorDelegate(UnityErrorDelegate);
        FanucControllerLibrary.Output.Instance.SetOutputDelegate(UnityOutputDelegate);
        FanucControllerLibrary.Output.Instance.SetLogDelegate(UnityOutputDelegate);
        FanucControllerLibrary.Output.Instance.SetErrorDelegate(UnityErrorDelegate);

        //Setup all components needed
        UICanvas = UICanvasGO.transform.GetComponentInChildren<UICanvas>();
        DevConsoleText = DevConsoleTextGameObject.GetComponent<Text>();
        Robot = RobotGO.GetComponent<Robot>();

        ARCamera.transform.localPosition = new Vector3(860, 860, 0);
        ARCamera.transform.localRotation = Quaternion.Euler(new Vector3(31.1f, -90, 1.29f));

        AddMsgToDevConsole("Init finished");
    }


    // Simulate Q values
    private bool dir = false;
    Point p;
    Vector3 correctionDelta = new Vector3(0f, 0f, 0f);
    private void Cycle()
    {

        Vector6 q = GetRobotJointPosition();

        if (Animate || Connected)
            Animator();


        //Update State
        Point[] joints = new PARRHI.HelperClasses.ForwardKinematics().CaluclateForwardKinematics(q);
        List<Vector3> joints_v3 = new List<Vector3>();
        foreach (var joint in joints)
        {
            var point_vector3 = TypeConversion.i.PointToVector3(joint) / 1000;
            joints_v3.Add(point_vector3);
        }
        Robot.UpdateJoints(joints_v3.ToArray());

        Joints = joints_v3;

        JointAngles = new double[6];
        JointAngles = q.Values.Cast<double>().ToArray();

        CameraPoint = TypeConversion.i.Vector3ToPoint(ARCamera.transform.localPosition);
        CameraPointV3 = ARCamera.transform.localPosition;
    }

    private Vector6 GetRobotJointPosition()
    {
        Vector6 q;
        if (!Connected)
        {
            double q3temp = q3 + q2;
            q = new Vector6(q1, q2, q3temp, q4, q5, q6);
        }
        else
        {
            q = RobotController.Commander.GetJointValues();
            q[2] = q[2] + q[1]; //Fanuc handles Joint 1 in a funny way
        }
        q[0] *= -1;
        q[3] *= -1;
        return q;
    }

    private bool About(double v1, double v2, double delta)
    {
        return (v1 >= v2 - delta && v1 < v2 + delta);
    }
    private bool About(Vector3 v1, Vector3 v2, double delta)
    {
        for (int i = 0; i < 3; i++)
        {
            if (About(v1[i], v2[i], delta) == false) return false;
        }
        return true;
    }
    private void Animator()
    {
        //animate q values
        float v = 0.01f;
        float d = 0.06f;
        q1 += q1 >= q1t + d ? -v : About(q1, q1t, d) ? 0 : v;
        q2 += q2 >= q2t + d ? -v : About(q2, q2t, d) ? 0 : v;
        q3 += q3 >= q3t + d ? -v : About(q3, q3t, d) ? 0 : v;
        q4 += q4 >= q4t + d ? -v : About(q4, q4t, d) ? 0 : v;
        q5 += q5 >= q5t + d ? -v : About(q5, q5t, d) ? 0 : v;
        q6 += q6 >= q6t + d ? -v : About(q6, q6t, d) ? 0 : v;

        //Move qtarget values;
        if (backToNull)
        {
            backToNull = false;
            q1t = 0;
            q2t = 0;
            q3t = 0;
            q4t = 0;
            q5t = 0;
            q6t = 0;
        }
        if (random)
        {
            System.Random rnd = new System.Random();
            random = false;
            q1t = RDouble(rnd, -2 * Mathf.PI, 2 * Mathf.PI);
            q2t = RDouble(rnd, -0.7, 1.6);
            q3t = RDouble(rnd, -1, 1);
            q4t = RDouble(rnd, -2 * Mathf.PI, 2 * Mathf.PI);
            q5t = RDouble(rnd, -1.5, 1.5);
            q6t = RDouble(rnd, -2 * Mathf.PI, 2 * Mathf.PI);
        }
    }
    private double RDouble(System.Random rnd, double min, double max)
    {
        return rnd.NextDouble() * (max - min) + min;
    }



    #region RobotInit
    /// <summary>
    /// Launches the robot initialization process
    /// </summary>
    public void InitRobotController()
    {
        if (!Connected)
        {
            RobotController = new FanucController();
            StartCoroutine(RobotConnectingCoRoutine());
        }
        else
        {
            RobotController.Commander.Exit();
        }
    }

    /// <summary>
    /// Performs an attempt to connect to the robot
    /// </summary>
    /// <returns></returns>
    public IEnumerator RobotConnectingCoRoutine()
    {
        int attempts = 0;
        for (int x = 0; x <= 15; x++)
        {
            bool success = RobotController.ConnectToRobotInSteps(ref attempts);
            if (!success)
                yield return new WaitForSeconds(0.1f);
            else
            {
                Connected = true;

                UnityOutputDelegate("Connected Successfully");
                yield break;
            }
        }
    }


    #endregion


    #region Output Delegates


    public void UnityOutputDelegate(string msg)
    {

        AddMsgToDevConsole(msg);

        Debug.Log(msg);
        System.Console.WriteLine(msg);
    }
    public void UnityErrorDelegate(string msg)
    {
        AddMsgToDevConsole("Error:" + msg);

        Debug.LogError(msg);
        Console.WriteLine("Error:" + msg);
    }

    #region DevConsole

    public List<string> outputs = new List<string>();
    private int counter = 0;
    private void AddMsgToDevConsole(string msg)
    {
        counter++;
        outputs.Add(msg);
        while (outputs.Count >= 9)
            outputs.RemoveAt(0);
        if (DevConsoleText != null)
        {
            string txt = "Dev Console Output:";
            for (int i = 0; i < outputs.Count; i++)
            {
                txt += $"\n{counter - outputs.Count + i}{outputs[i]}";
            }
            DevConsoleText.text = txt;
        }
    }
    #endregion

    #endregion


    #region Step Mechanics


    //Variables available for program execution
    Point CameraPoint;
    Vector3 CameraPointV3;
    List<Vector3> Joints;
    double[] JointAngles;
    private float timeSinceStepActivation { get { return Time.timeSinceLevelLoad - timeOfStepActivation; } }
    //End of variables


    private int step = -1;
    private float timeOfStepActivation = 0;

    /// <summary>
    /// Performs one step in the application logic
    /// </summary>
    /// <returns></returns>
    private bool PerformTaskStep()
    {
        var stepFinished = StepCompleted(step);
        if (stepFinished)
        {
            AddMsgToDevConsole($"Step {step} finished");
            timeOfStepActivation = Time.timeSinceLevelLoad;
            step++;
            StepChange(step);
        }
        return DoStep(step);
    }

    private bool StepCompleted(int index)
    {
        switch (index)
        {
            case -1:
                return true;
            case 0:
                return About(CameraPointV3, FindGOPos("Step0_UserTarget"), 200);
            case 1:
                return About(FindGOPos("Joint (5)"), FindGOPos("Step1_RobotTCPTarget"), 50);
            case 2:
                return About(FindGOPos("Joint (5)"), FindGOPos("Step2_RobotTCPTarget"), 50);
            case 3:
                return About(CameraPointV3, FindGOPos("Step3_UserTarget"), 200);
            case 4:
                return timeSinceStepActivation > 5;
            case 5:
                return JointAngles.All(x => About(x, 0, 0.1));
            case 6:
                return timeSinceStepActivation > 5;
                break;
            case 7:
                break;
        }
        return true;
    }

    private bool DoStep(int index)
    {
        switch (index)
        {
            case 0:
                SetUIText("Move to the marked safety area marked in blue");
                break;
            case 1:
                SetUIText("Good!\nNow Switch on the robot, and hit the connect button.\nWhen you are done, jog the robot to the area marked by the blue sphere");
                break;
            case 2:
                SetUIText("Good!\nNow carefully take out the part that is gripped by the robot and then jog it into the far away area marked by the green sphere");
                break;
            case 3:
                SetUIText("Ok.\nNow move away into the area marked with the blue sphere. We will then move the robot via the Robot Library!");
                break;
            case 4:
                SetUIText("Since you are here now, we will wait for you to turn around and in a few seconds the library will take over the robot :) ");
                break;
            case 5:
                SetUIText("Here we go! Lets move the robot back into his zero-position");
                MoveRobot(new double[] { 0, 0, 0, 0, 0, 0 });
                break;
            case 6:
                SetUIText("Ok cool. Robot is in its home position.");
                break;
            case 7:
                SetUIText("Very well done. You may now close the HoloLens application!");
                break;
        }
        return index <= 7;
    }

    private void SetUIText(string msg)
    {
        UICanvas.SetUIText($"Step {step}: {msg}");
        UITextCanvasObject.GetComponent<Text>().text = $"Step {step}: {msg}";
    }

    private void StepChange(int index)
    {
        var children = FindChildren(TaskObjects.transform, $"Step{index}");
        var childrenNotInStep = FindChildrenNotInStep(TaskObjects.transform, $"Step{index}");
        foreach (var item in children)
        {
            item.gameObject.SetActive(true);
        }
        foreach (var item in childrenNotInStep)
        {
            item.gameObject.SetActive(false);
        }
    }

    #region Helpers


    private List<Transform> FindChildrenNotInStep(Transform transform, string nameToSearch)
    {
        return transform.GetComponentsInChildren<Transform>(true).Where(t => !t.name.StartsWith(nameToSearch) && t.name.StartsWith("Step")).ToList();
    }

    public List<Transform> FindChildren(Transform transform, string nameToSearch)
    {
        return transform.GetComponentsInChildren<Transform>(true).Where(t => t.name.StartsWith(nameToSearch)).ToList();
    }

    Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>();
    public GameObject FindGO(string name)
    {
        if (objects.Any(x => x.Key == name)) return objects[name];
        else
        {
            var obj = GameObject.Find(name);
            objects[name] = obj;
            return obj;
        }
    }
    public Vector3 FindGOPos(string name)
    {
        return FindGO(name).transform.localPosition;
    }

    #endregion
    #endregion


    #region Robot Libaray
    public void MoveRobot(double[] jointAngles)
    {
        if (Connected)
        {
            RobotController.Commander.MoveAbsolute(new Vector6(jointAngles));
        }
        else
        {
            Animate = true;
            q1t = jointAngles[0];
            q2t = jointAngles[1];
            q3t = jointAngles[2];
            q4t = jointAngles[3];
            q5t = jointAngles[4];
            q6t = jointAngles[5];
        }
    }
    #endregion
}
