using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomMoonLightChange : MonoBehaviour
{
   
    GameObject mL_1;
    GameObject mL_2;

    void Start()
    {

        StartCoroutine("BackAndForth");

    }

    IEnumerator BackAndForth()
    {

        while (enabled)
        {

            yield return null;


        }


    }

}
