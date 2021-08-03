using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextTrigger : MonoBehaviour {

    [Multiline(8)]public string text = "Hello, World!";
    public string promptString = "? (A)";
    public UnityEvent showPrompt;
    public UnityEvent hidePrompt;
    
    bool prompting;

    void OnTriggerEnter2D (Collider2D c)
    {
        if (prompting || c.GetComponentInParent<PlayerController>() == null) return;

        prompting = true;
        showPrompt.Invoke();
        TextUIController.ShowPrompt(promptString);
        
    }

    void OnTriggerExit2D (Collider2D c)
    {
        if (!prompting || c.GetComponentInParent<PlayerController>() == null) return;

        prompting = false;
        hidePrompt.Invoke();
        TextUIController.HidePrompt();
    }

    void Update ()
    {
        if (Time.timeScale != 0 && prompting && Input.GetButtonDown("Submit"))
        {
            TextUIController.WaitText(text);
        }
    }
}