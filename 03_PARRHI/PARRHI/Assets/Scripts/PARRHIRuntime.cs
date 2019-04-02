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

public class PARRHIRuntime : MonoBehaviour
{

    Container Container;
    FanucController RobotController;

    UICanvas UICanvas;

    //Set by Unity editor
    public GameObject MainCamera;

    public bool ConnectEnabled;
    private bool ConnectionProcessStarted = false;
    private bool Connected;

    public int q1;
    public int q2;
    public int q3;
    public int q4;
    public int q5;
    public int q6;

    public bool animate;
    public bool random;
    public bool backToNull;
    public int q1t;
    public int q2t;
    public int q3t;
    public int q4t;
    public int q5t;
    public int q6t;

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
        Cycle();

        if (animate)
        {
            Animator();
        }

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
        UICanvas = this.transform.GetComponentInChildren<UICanvas>();
    }

    /// <summary>
    /// Initializes data
    /// </summary>
    private void InitializeSystem()
    {
        DataImport importer = new DataImport();
        Container = importer.Import(@"C:\Users\ericv\Documents\TUM\BA\PARRHI\03_PARRHI\PARRHI\Assets\New Folder\InputData.xml");

        //Setup Container Delegates
        Container.State.World.SetUITextDelegate = UICanvas.SetUIText;

        //Write Errors to UI
        UICanvas.SetErrors(importer.XMLValidationResult.GetAllErrors());

        //Instantiate all holograms
        AddHolograms(Container);
    }

    // Simulate Q values
    private void Cycle()
    {
        double q3temp = q3 + q2;
        Vector6 q = new Vector6(q1, q2, q3temp, q4, q5, q6);

        //Update State
        Container.Update(q, TypeConversion.i.Vector3ToPoint(MainCamera.transform.position), (long)Time.realtimeSinceStartup);
    }

    private void Animator()
    {
        //animate q values

        q1 += q1 > q1t ? -1 : (q1 == q1t) ? 0 : 1;
        q2 += q2 > q2t ? -1 : (q2 == q2t) ? 0 : 1;
        q3 += q3 > q3t ? -1 : (q3 == q3t) ? 0 : 1;
        q4 += q4 > q4t ? -1 : (q4 == q4t) ? 0 : 1;
        q5 += q5 > q5t ? -1 : (q5 == q5t) ? 0 : 1;
        q6 += q6 > q6t ? -1 : (q6 == q6t) ? 0 : 1;

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
            q1t = rnd.Next(-180, 180);
            q2t = rnd.Next(-180, 180);
            q3t = rnd.Next(-180, 180);
            q4t = rnd.Next(-180, 180);
            q5t = rnd.Next(-180, 180);
            q6t = rnd.Next(-180, 180);
        }
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
        RobotController = new FanucController();
        StartCoroutine(RobotConnectingCoRoutine());
    }

    /// <summary>
    /// Performs an attempt to connect to the robot
    /// </summary>
    /// <returns></returns>
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
        Debug.Log(msg);
        System.Console.WriteLine(msg);
    }
    public void UnityErrorDelegate(string msg)
    {
        Debug.LogError(msg);
        Console.WriteLine("Error:" + msg);
    }
    #endregion


}
