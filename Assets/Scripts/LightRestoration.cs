using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRestoration : MonoBehaviour
{
    
    bool inLight = false;

    public MagicController magic;

    void start()
    {

        magic = GetComponent<MagicController>();
        
    }

    void OnTriggerEnter2D (Collider2D c)
    {

        inLight = true;

        RestoreMana();

    }

    void OnTriggerExit2D (Collider2D c)
    {

        inLight = false;

    }

    void RestoreMana()
    {

        while (inLight == true && magic.mana != magic.maxMana)
        {

            magic.mana += 0.1f;

        }


    }


}
