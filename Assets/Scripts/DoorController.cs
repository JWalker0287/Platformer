using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool inTrigger = false;
    public string interactButton = "e";
    void Update()
    {
        if(Input.GetKeyDown(interactButton) && inTrigger)
        {
            Debug.Log("End of level");
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        PlayerController p = c.gameObject.GetComponent<PlayerController>();
        if (p == null) return;
        inTrigger = true;
    }

    void OnTriggerExit2D(Collider2D c)
    {
        PlayerController p = c.gameObject.GetComponent<PlayerController>();
        if (p == null) return;
        inTrigger = false;
    }
}
