using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEvent : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<Animator>().Play(0);
    }
    public void Disable ()
    {
        gameObject.SetActive(false);
    }
}
