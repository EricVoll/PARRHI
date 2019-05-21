using PARRHI.Objects.Points;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public List<GameObject> JointGOs;
    public List<GameObject> AxeGOs;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        
    }

    public void UpdateJoints(Vector3[] joints)
    {
        for (int i = 0; i < JointGOs.Count; i++)
        {
            JointGOs[i].transform.position = joints[i];
        }

        for (int i = 0; i < JointGOs.Count - 1; i++)
        {
            SetAxe(joints[i], joints[i + 1], AxeGOs[i]);
        }
    }

    private void SetAxe(Vector3 Point1, Vector3 Point2, GameObject axe)
    {
        Vector3 d = (Point2 - Point1) / 2f;

        float scaleX = axe.transform.localScale.x;
        float scaleY = axe.transform.localScale.z;

        axe.transform.localRotation = Quaternion.LookRotation(d) * Quaternion.Euler(90, 0, 0);

        axe.transform.localScale = new Vector3(scaleX, d.magnitude * 1000, scaleY);

        axe.transform.position = Point1 + d;
    }
}
