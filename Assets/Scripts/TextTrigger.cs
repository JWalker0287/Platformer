using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextTrigger : MonoBehaviour {

    [Multiline(8)]public string text = "Hello, World!";
    public UnityEvent showPrompt;
    public UnityEvent hidePrompt;
    
    bool prompting;

    void Awake ()
    {
        hidePrompt.Invoke();
    }

    void OnTriggerEnter2D (Collider2D c)
    {
        if (prompting || c.GetComponentInParent<PlayerController>() == null) return;

        prompting = true;
        showPrompt.Invoke();
        
    }

    void OnTriggerExit2D (Collider2D c)
    {
        if (!prompting || c.GetComponentInParent<PlayerController>() == null) return;

        prompting = false;
        hidePrompt.Invoke();
    }

    void Update ()
    {
        if (Time.timeScale != 0 && prompting && Input.GetButtonDown("Submit"))
        {
            TextUIController.WaitText(text);
        }
    }
}