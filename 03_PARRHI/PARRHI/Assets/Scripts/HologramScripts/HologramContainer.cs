using PARRHI.Objects.Holograms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramContainer : MonoBehaviour
{

    //Set by Unity editor
    public GameObject CylinderPrefab;
    public GameObject SpherePrefab;

    readonly List<GameObject> Cylinders = new List<GameObject>();
    readonly List<GameObject> Spheres = new List<GameObject>();

    public void RemoveAllChildren()
    {
        if (transform.childCount > 0)
        {
            Debug.Log($"Removeing all children of {this.name}");
            while (transform.childCount > 0)
            {
                var go = transform.GetChild(0);
                go.parent = null;
                GameObject.Destroy(go.gameObject);
            }
        }
    }


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
