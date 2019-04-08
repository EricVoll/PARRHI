using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReimportBtnScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PARRHIRuntimeGameObject;

    public void ButtonClickEvent()
    {
        if (PARRHIRuntimeGameObject == null)
        {
            Debug.LogError($"Could not reset {typeof(PARRHIRuntime)} instance, because the gameobject {nameof(PARRHIRuntimeGameObject)} was null.");
            return;
        }

        var paarhiRuntime = PARRHIRuntimeGameObject.GetComponent<PARRHIRuntime>();
        paarhiRuntime.Reset();
    }
}
