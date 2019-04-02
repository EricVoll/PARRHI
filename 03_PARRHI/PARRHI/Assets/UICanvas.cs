using PARRHI.HelperClasses.XML;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour
{
    // Start is called before the first frame update

    Text Text;
    public GameObject ErrorContentParent;
    public GameObject ErrorElementPrefab;

    void Start()
    {
        Text = this.transform.GetComponentInChildren<Text>();
    }

    public void SetUIText(string text)
    {
        Text.text = text;
    }

    public void SetErrors(List<XMLValidationError> Errors)
    {
        foreach (var error in Errors)
        {
            GameObject errorGameObject = GameObject.Instantiate(ErrorElementPrefab);
            errorGameObject.transform.parent = ErrorContentParent.transform;
            var sc = errorGameObject.GetComponent<ErrorElementScript>();
            sc.SetText($"Message {Errors.IndexOf(error)}: {error.Severity.ToString()}", error.Message);
        }
    }
}
