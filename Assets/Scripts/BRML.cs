using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRML : MonoBehaviour
{

    SpriteRenderer mLSR;

    Color mLC;

    float mLAlphaMax = 0.45f;

    void Awake()
    {

        mLSR = GetComponent<SpriteRenderer>();
        
        mLC.r = 225f;
        mLC.g = 225f;
        mLC.b = 225f;
        mLC.a = 0f;
        
        mLSR.color = mLC;

    }


    IEnumerator BrightenDarken()
    {


        for (float i = 0f; i < mLAlphaMax; i += 0.01f)
        {

            Debug.Log(i);

            mLC.a = i;

            mLSR.color = mLC;

            yield return new WaitForSeconds(0.01f);

        }

        yield return new WaitForSeconds(5);

        for (float i = 0.50f; i > 0f; i-= 0.01f)
        {

            Debug.Log(i);

            mLC.a = i;
            
            mLSR.color = mLC;

            yield return new WaitForSeconds(0.01f);

        }

        yield return new WaitForSeconds(5);

    }





}
