using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{

    public TextUIController textBoxUI;
    
    void OnTriggerEnter2D(Collider2D c)
    {

        textBoxUI.SignInteraction();

    }

    void OnTriggerExit2D(Collider2D c)
    {

        textBoxUI.textBox.enabled = false;

    }

}
