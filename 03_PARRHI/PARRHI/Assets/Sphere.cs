using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public PARRHI.Objects.Holograms.Sphere sphere;

    // Update is called once per frame
    void Update()
    {
        if (!Config.i.updateHolograms) return;
        OrientateSphere();
    }

    private void OrientateSphere()
    {
        Vector3 Point1 = TypeConversion.i.PointToVector3(sphere.Point1);
        float radius = (float)sphere.Radius;

        this.transform.localScale = Vector3.one * radius;

        this.transform.position = Point1;
    }
}
