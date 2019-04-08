using PARRHI.Objects.Points;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TypeConversion : MonoBehaviour
{
    public static TypeConversion i;
    // Start is called before the first frame update
    void Awake()
    {
        i = this;
    }

    public Vector3 PointToVector3(Point p)
    {
        return new Vector3((float)p.X, (float)p.Y, (float)p.Z);
    }

    internal Point Vector3ToPoint(Vector3 v)
    {
        return new Point(v.x, v.y, v.z);
    }
}
