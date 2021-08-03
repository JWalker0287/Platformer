using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUIController : MonoBehaviour
{
    public string name;
    public GameObject inputfield;
    public GameObject textDisplay;

    public void StoreName()
    {
        name = inputfield.GetComponent<Text>().text;
        textDisplay.GetComponent<Text>().text = "Hello knight " + name + " Welcome ";
    }
}
