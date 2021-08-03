using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurabilityBar : MonoBehaviour
{
   public float speed = 10;
    public SwordController sword;
    public Image bar;

    void Awake()
    {

        bar = GetComponent<Image>();

    }

    void Update()
    {

        if (sword == null)
        {

            sword = PlayerController.player.GetComponent<SwordController>();

        }
        

        bar.fillAmount = Mathf.Lerp(bar.fillAmount, sword.durabilityPct, Time.deltaTime * speed);
    
    }
}
