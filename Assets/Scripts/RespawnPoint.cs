using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
   
   void OnDrawGizmos ()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position,transform.localScale);
    }

    void OnTriggerEnter2D (Collider2D c)
    {

        PlayerController.player.defaultPosition = PlayerController.player.transform.position;

    }

}
