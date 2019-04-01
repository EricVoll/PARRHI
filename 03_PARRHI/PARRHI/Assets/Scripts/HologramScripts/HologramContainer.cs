using PARRHI.Objects.Holograms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramContainer : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject CylinderPrefab;
    public GameObject SpherePrefab;

    List<GameObject> Cylinders = new List<GameObject>();
    List<GameObject> Spheres = new List<GameObject>();


    public void AddCylinder(Zylinder zylinder)
    {
        var gameobj = GameObject.Instantiate(CylinderPrefab);
        gameobj.transform.parent = this.transform;
        gameobj.GetComponent<Cylinder>().Zylinder = zylinder;
        Cylinders.Add(gameobj);
    }

    public void AddSphere(PARRHI.Objects.Holograms.Sphere sphere)
    {
        var gameobj = GameObject.Instantiate(SpherePrefab);
        gameobj.transform.parent = this.transform;
        gameobj.GetComponent<Sphere>().sphere = sphere;
        Spheres.Add(gameobj);
    }
}
