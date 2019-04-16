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

public class PARRHIRuntime : MonoBehaviour
{

    Container Container;
    FanucController RobotController;

    UICanvas UICanvas;

    //Set by Unity editor
    public GameObject MainCamera;
    public TextAsset xmlFile;
    public TextAsset xsdFile;
    public GameObject UICanvasGO;
    public GameObject DevConsoleTextGameObject;
    private Text DevConsoleText;

    public bool ConnectEnabled;
    private bool ConnectionProcessStarted = false;
    private bool Connected;
    public bool SetDirection;

    public double q1;
    public double q2;
    public double q3;
    public double q4;
    public double q5;
    public double q6;

    public bool animate;
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

    private void Start()
    {
        InitializeSystem();
    }

    // Update is called once per frame
    void Update()
    {

        if (ConnectEnabled && !ConnectionProcessStarted)
        {
            ConnectionProcessStarted = true;
            ConnectEnabled = false;
            InitRobotController();
        }

        Cycle();
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
    }

    /// <summary>
    /// Initializes data
    /// </summary>
    private void InitializeSystem()
    {
        DataImport importer = new DataImport();

        Container = importer.Import(xmlFile.text);

        //Setup Container Delegates
        Container.State.World.SetUITextDelegate = UICanvas.SetUIText;

        //Write Errors to UI
        UICanvas.SetErrors(importer.XMLValidationResult.GetAllErrors());

        //Instantiate all holograms
        AddHolograms(Container);
    }

    // Simulate Q values
    private bool dir = false;
    private void Cycle()
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

            if (SetDirection)
            {
                SetDirection = false;
                if (dir)
                {
                    RobotController.Commander.MoveAbsolute(new Vector6(685, -125, 394, -166, -68, -14));
                    dir = false;
                }
                else
                {
                    RobotController.Commander.MoveAbsolute(new Vector6(685, 397, 394, -166, -68, -14));
                    dir = true;
                }
            }
        }
        if (true)
            Animator();

        //Update State
        Container.Update(q, TypeConversion.i.Vector3ToPoint(MainCamera.transform.position), (long)Time.realtimeSinceStartup);
    }

    private bool About(double v1, double v2, double delta)
    {
        return (v1 >= v2 - delta && v1 < v2 + delta);
    }
    private void Animator()
    {
        //animate q values
        float v = 0.05f;
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

    public void Reset()
    {
        Container = null;
        q1 = 0;
        q2 = 0;
        q3 = 0;
        q4 = 0;
        q5 = 0;
        q6 = 0;
        q1t = 0;
        q2t = 0;
        q3t = 0;
        q4t = 0;
        q5t = 0;
        q6t = 0;
        ConnectEnabled = false;
        Connected = false;
        ConnectionProcessStarted = false;

        InitializeSystem();
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


    #region Holograms
    /// <summary>
    /// Passes on all hologram objects to the respective container
    /// </summary>
    /// <param name="container"></param>
    private void AddHolograms(Container container)
    {
        HologramContainer hContainer = this.GetComponentInChildren<HologramContainer>();

        hContainer.RemoveAllChildren();

        foreach (var item in container.Holograms.Where(x => x is PARRHI.Objects.Holograms.Sphere).Cast<PARRHI.Objects.Holograms.Sphere>())
        {
            hContainer.AddSphere(item);
        }
        foreach (var item in container.Holograms.Where(x => x is PARRHI.Objects.Holograms.Zylinder).Cast<PARRHI.Objects.Holograms.Zylinder>())
        {
            hContainer.AddCylinder(item);
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
        while(outputs.Count >= 9)
            outputs.RemoveAt(0);
        if(DevConsoleText != null)
        {
            string txt = "Dev Console Output:";
            for (int i = 0; i < outputs.Count; i++)
            {
                txt += $"\n{counter-outputs.Count + i}{outputs[i]}";
            }
            DevConsoleText.text = txt;
        }
    }
    #endregion

    #endregion


}
