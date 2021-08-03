using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTrigger : MonoBehaviour
{
    public bool inTrigger = false;
    public string interactButton = "e";
    public string message = "You Are Reading a Sign";
    public TextMesh interactDisplay;

    void Awake()
    {
        interactDisplay.text = interactButton;
        interactDisplay.gameObject.SetActive(false);
    }

    void Update()
    {
        if(inTrigger)
        {
            if(Input.GetKeyDown(interactButton))
            {
                TextUIController.textUI.DisplayText(message);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        PlayerController p = c.gameObject.GetComponent<PlayerController>();
        if (p == null) return;
        inTrigger = true;
        interactDisplay.gameObject.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D c)
    {
        PlayerController p = c.gameObject.GetComponent<PlayerController>();
        if (p == null) return;
        inTrigger = false;
        interactDisplay.gameObject.SetActive(false);
    }
}
