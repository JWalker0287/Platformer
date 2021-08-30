using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : Interactable {

    [Multiline(8)]public string text = "Hello, World!";

    public override void Interact()
    {
        TextUIController.WaitText(text);
    }
}