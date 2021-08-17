using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent showPrompt;
    public UnityEvent hidePrompt;
    
    protected bool prompting;

    public virtual void Interact()
    {
        Debug.Log("Override me.");
    }

    public virtual bool CanInteract()
    {
        return (Time.timeScale != 0 && prompting);
    }

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
}
