using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public string text = "Hello, World!";

    void OnTriggerEnter (Collider c)
    {
        if (!c.CompareTag("Player")) return;

        TextUIController.ShowText(text);
    }
}
