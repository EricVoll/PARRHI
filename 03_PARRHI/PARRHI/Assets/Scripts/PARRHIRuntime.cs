using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PARRHI;
using PARRHI.Objects.State;
using FanucControllerLibrary;
using FanucControllerLibrary.DataTypes;
using PARRHI.Objects.Points;

public class PARRHIRuntime : MonoBehaviour
{

    State state;
    public FanucController RobotController;
    public bool ConnectEnabled;
    private bool ConnectionProcessStarted = false;
    private bool Connected;

    public uint q1;
    public uint q2;
    public uint q3;
    public uint q4;
    public uint q5;
    public uint q6;

    public GameObject[] Joints;

    private void Awake()
    {
        state = new State();
        RobotController = new FanucController();


        PARRHI.Output.Instance.SetOutputDelegate(UnityOutputDelegate);
        FanucControllerLibrary.Output.Instance.SetOutputDelegate(UnityOutputDelegate);
    }

    // Update is called once per frame
    void Update()
    {
        Cycle();
        return;

        if (ConnectEnabled && !ConnectionProcessStarted)
        {
            ConnectionProcessStarted = true;
            ConnectEnabled = false;
            InitRobotController();
        }
        if (Connected)
        {
            Cycle();
        }
    }

    public void InitRobotController()
    {
        RobotController = new FanucController();
        StartCoroutine(RobotConnectingCoRoutine());
    }

    public IEnumerator RobotConnectingCoRoutine()
    {
        int attempts = 0;
        for (int x = 0; x <= 100; x++)
        {
            bool success = RobotController.ConnectToRobotInSteps(ref attempts);
            if (success == false)
                yield return new WaitForSeconds(0.1f);
            else
            {
                Connected = true;
                UnityOutputDelegate("Connected Successfully");
                yield break;
            }
        }
    }

    public void UnityOutputDelegate(string msg)
    {
        Debug.Log(msg);
        System.Console.WriteLine(msg);
    }

    //private void Cycle()
    //{
    //    //Vector6 q = RobotController.Commander.GetJointValues();
    //    Vector6 q = RobotController.Commander.GetForces();
    //    state.UpdateState(q, null);

    //    Point[] joints = state.Robot.Joints;

    //    if (joints.Length >= 5)
    //        UnityOutputDelegate($"TCP: X:{joints[5].X} Y:{joints[5].Y} Z:{joints[5].Z}");

    //    for (int i = 0; i < joints.Length; i++)
    //    {
    //        Joints[i].transform.position = new Vector3((float)joints[i].X, (float)joints[i].Y, (float)joints[i].Z);
    //    }

    //}


    // Simulate Q values
    private void Cycle()
    {
        //Vector6 q = RobotController.Commander.GetJointValues();
        double q3temp = q3 + q2;
        Vector6 q = new Vector6(q1, q2, q3temp, q4, q5, q6);
        state.UpdateState(q, null);

        Point[] joints = state.Robot.Joints;

        if (joints.Length >= 5)
            UnityOutputDelegate($"TCP: X:{joints[5].X} Y:{joints[5].Y} Z:{joints[5].Z}");

        for (int i = 0; i < joints.Length; i++)
        {
            Joints[i].transform.position = new Vector3((float)joints[i].X, (float)joints[i].Z, (float)joints[i].Y);
        }

    }
}
