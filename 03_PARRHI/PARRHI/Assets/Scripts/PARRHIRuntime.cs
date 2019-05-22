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
    /// <summary>
    /// Instance of Container used for the PARRHI system. Responsible for the calls into the PARRHI library
    /// </summary>
    Container Container;

    /// <summary>
    /// The Controller that will handle the communication with the robot
    /// </summary>
    FanucController RobotController;

    UICanvas UICanvas;

    //Set by Unity editor

    /// <summary>
    /// XML File to import
    /// </summary>
    public TextAsset xmlFile;

    /// <summary>
    /// XSD File to use for validation
    /// </summary>
    public TextAsset xsdFile;

    /// <summary>
    /// Camera GameObject used for Camera Point
    /// </summary>
    public GameObject ARCamera;

    /// <summary>
    /// User UI Canvas GameObject. Used for PARRHI UI output
    /// </summary>
    public GameObject UICanvasGO;

    #region Backend GameObjects

    /// <summary>
    /// Dev Console output GameObject.
    /// </summary>
    public GameObject DevConsoleTextGameObject;
    /// <summary>
    /// Dev Console Text object to write content to
    /// </summary>
    private Text DevConsoleText;

    /// <summary>
    /// GameObject of the Robot's marker image
    /// </summary>
    public GameObject RobotTrackerGO;

    /// <summary>
    /// GameObject that will contain all PARRHI Holograms
    /// </summary>
    public GameObject HologramContainer;

    #endregion
    public bool ConnectEnabled;

    /// <summary>
    /// True if the FanucController is connected to the robot's network socket
    /// </summary>
    private bool Connected;


    #region Simulation & Animation variables


    /// <summary>
    /// If true, then simulated values will be animated in steps
    /// </summary>
    public bool Animate;

    /// <summary>
    /// Selectes random values for qit
    /// </summary>
    public bool Random;

    /// <summary>
    /// Sets 0 for all qit
    /// </summary>
    public bool BackToNull;

    public double q1;
    public double q2;
    public double q3;
    public double q4;
    public double q5;
    public double q6;

    public double q1t;
    public double q2t;
    public double q3t;
    public double q4t;
    public double q5t;
    public double q6t;

    #endregion

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
        //Used to connect to robot in Unity devloper runtime
        if (ConnectEnabled && !Connected)
        {
            //Reset checkbox
            ConnectEnabled = false;
            InitRobotController();
        }

        //If container is null, we dont have to start because nothing would work
        if (Container != null)
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

        //Container = importer.Import(xmlFile.text);
        Container = importer.Import(@"C:\Users\ericv\Documents\TUM\BA\PARRHI\03_PARRHI\PARRHI\Assets\New Folder\ParametrisedProgam_Evaluation.xml", @"C:\Users\ericv\Documents\TUM\BA\PARRHI\03_PARRHI\PARRHI\Assets\New Folder\parrhiScheme.xsd");

        if (Container != null)
        {
            //Setup Container Delegates
            Container.State.World.SetUITextDelegate = UICanvas.SetUIText;

            //Instantiate all holograms
            AddHolograms(Container);
        }

        //Write Errors to UI
        UICanvas.SetErrors(importer.XMLValidationResult.GetAllErrors());

    }

    // Simulate Q values
    Vector3 correctionDelta = new Vector3(0f, 0f, 0f);
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
            q[2] = q[2] + q[1]; //Fanuc handles Joint 1 in a funny way
        }
        if (Animate || Connected)
            Animator();

        //Calculate cam pos vector

        Vector3 ImageToCam = ARCamera.transform.localPosition - RobotTrackerGO.transform.localPosition + correctionDelta;

        ImageToCam = HologramContainer.transform.InverseTransformPoint(ImageToCam);

        //Feedback loop and correcting some values
        var v = ARCamera.transform.position - HologramContainer.transform.TransformPoint(ImageToCam);
        correctionDelta += v * 0.2f;


        Point camPoint = TypeConversion.i.Vector3ToPoint(ImageToCam);


        //Update State
        q[0] *= -1;
        q[3] *= -1;
        Container.Update(q, camPoint, (long)Time.realtimeSinceStartup);
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
        if (BackToNull)
        {
            BackToNull = false;
            q1t = 0;
            q2t = 0;
            q3t = 0;
            q4t = 0;
            q5t = 0;
            q6t = 0;
        }
        if (Random)
        {
            System.Random rnd = new System.Random();
            Random = false;
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
            {
                AddMsgToDevConsole($"Connection failed. Attempt {attempts}");
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                Connected = true;

                UnityOutputDelegate("Connected Successfully");
                AddMsgToDevConsole($"Connected Successfully");
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


}


/*
 
    To real points for the robot to drive to
     685, -125, 394, -166, -68, -14
     685, 397, 394, -166, -68, -14
     
     */
