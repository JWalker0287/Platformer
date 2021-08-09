using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRestoration : MonoBehaviour
{
    bool inLight = false;

    MagicController magic;
    void OnTriggerEnter2D (Collider2D c)
    {
        magic = c.GetComponentInParent<MagicController>();
        inLight = true;
    }

    void OnTriggerExit2D (Collider2D c)
    {
        magic = null;
        inLight = false;
    }

    void Update()
    {
        if (inLight == true && magic != null && magic.mana != magic.maxMana)
        {
            magic.mana += 0.1f;
        }
    }


}
