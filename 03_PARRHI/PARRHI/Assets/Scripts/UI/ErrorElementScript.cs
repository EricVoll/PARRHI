using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorElementScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject HeaderTextGO;
    public GameObject BodyTextGO;
    private Text HeaderText;
    private Text BodyText;
    void Awake()
    {
        HeaderText = HeaderTextGO.GetComponent<Text>();
        BodyText = BodyTextGO.GetComponent<Text>();
    }

    public void SetText(string header, string body)
    {
        HeaderText.text = header;
        BodyText.text = body;
    }
}
