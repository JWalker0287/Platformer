using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public bool inTrigger = false;
    public string interactButton = "e";
    void Update()
    {
        if(Input.GetKeyDown(interactButton) && inTrigger)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
