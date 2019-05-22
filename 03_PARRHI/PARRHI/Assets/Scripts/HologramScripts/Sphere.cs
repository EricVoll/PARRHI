using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    private PARRHI.Objects.Holograms.Sphere sphere;

    // Update is called once per frame
    void Update()
    {
        if (!Config.i.updateHolograms) return;
        OrientateSphere();
        this.GetComponent<Renderer>().enabled = sphere.Active;
    }
    public void SetData(PARRHI.Objects.Holograms.Sphere Sphere)
    {
        sphere = Sphere;
    }

    private void OrientateSphere()
    {
        Vector3 Point1 = TypeConversion.i.PointToVector3(sphere.Point1);
        float radius = (float)sphere.Radius;
        

        this.transform.localScale = Vector3.one * radius;

        this.transform.localPosition = Point1;
    }
}
