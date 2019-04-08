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
    public GameObject CloseButton;  

    void Start()
    {
        Text = this.transform.GetComponentInChildren<Text>();
    }

    /// <summary>
    /// Action to set UI Text
    /// </summary>
    /// <param name="text"></param>
    public void SetUIText(string text)
    {
        Text.text = text;
    }

    public void CollapseErrorSection()
    {
        CloseButton.GetComponent<Button>().onClick.Invoke();
    }

    public void SetErrors(List<XMLValidationError> Errors)
    {
        //Remove all existing errors
        while (ErrorContentParent.transform.childCount > 0)
        {
            var child = ErrorContentParent.transform.GetChild(0);
            child.parent = null;
            GameObject.Destroy(child.gameObject);
        }

        if (Errors.Count > 0)
        {
            //Add new errors
            foreach (var error in Errors)
            {
                GameObject errorGameObject = GameObject.Instantiate(ErrorElementPrefab);
                errorGameObject.transform.parent = ErrorContentParent.transform;
                var sc = errorGameObject.GetComponent<ErrorElementScript>();
                sc.SetText($"Message {Errors.IndexOf(error)}: {error.Severity.ToString()}", error.Message);
            }
        }
        else
        {
            CollapseErrorSection();
        }
    }
}
