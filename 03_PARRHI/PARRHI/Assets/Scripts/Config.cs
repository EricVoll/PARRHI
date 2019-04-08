using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    // Start is called before the first frame update
    public static Config i;
    void Awake  ()
    {
        i = this;
        updateHolograms = true;
    }

    public bool updateHolograms = true;
}
