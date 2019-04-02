using PARRHI.Objects.Points;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    // Start is called before the first frame update


    private PARRHI.Objects.Holograms.Zylinder Zylinder;

    // Update is called once per frame
    void Update()
    {
        if (!Config.i.updateHolograms) return;
        OrientateCylinder();
    }

    public void SetData(PARRHI.Objects.Holograms.Zylinder zylinder)
    {
        Zylinder = zylinder;
    }

    private void OrientateCylinder()
    {
        Vector3 Point1 = TypeConversion.i.PointToVector3(Zylinder.Point1);
        Vector3 Point2 = TypeConversion.i.PointToVector3(Zylinder.Point2);
        float radius = (float)Zylinder.Radius;

        this.transform.LookAt(Point2, Vector3.left);
        this.transform.Rotate(new Vector3(1, 0, 0), 90);

        this.transform.localScale = new Vector3(radius, (Point2 - Point1).magnitude / 2, radius);

        this.transform.localPosition = Point1 + 0.5f * (Point2 - Point1);
    }
}
