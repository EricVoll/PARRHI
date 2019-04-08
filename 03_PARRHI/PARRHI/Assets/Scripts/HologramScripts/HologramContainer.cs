using PARRHI.Objects.Holograms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramContainer : MonoBehaviour
{

    //Set by Unity editor
    public GameObject CylinderPrefab;
    public GameObject SpherePrefab;
    public GameObject CylinderPrefabTransparent;
    public GameObject SpherePrefabTransparent;

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
        GameObject gameobj;
        if (zylinder.RenderMode == "transparent")
            gameobj = GameObject.Instantiate(CylinderPrefabTransparent);
        else
            gameobj = GameObject.Instantiate(CylinderPrefab);

        gameobj.transform.parent = this.transform;
        gameobj.GetComponent<Cylinder>().SetData(zylinder);
        Cylinders.Add(gameobj);
    }

    public void AddSphere(PARRHI.Objects.Holograms.Sphere sphere)
    {
        GameObject gameobj;
        if (sphere.RenderMode == "transparent")
            gameobj = GameObject.Instantiate(SpherePrefabTransparent);
        else
            gameobj = GameObject.Instantiate(SpherePrefab);
        gameobj.transform.parent = this.transform;
        gameobj.GetComponent<Sphere>().SetData(sphere);
        Spheres.Add(gameobj);
    }
}
