using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour {

    [Multiline(8)]public string text = "Hello, World!";

    void OnTriggerEnter2D (Collider2D c)
    {
        if (c.GetComponentInParent<PlayerController>() == null) return;

        TextUIController.ShowText(text);
    }
}