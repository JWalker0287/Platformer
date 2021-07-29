using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicBar : MonoBehaviour
{
    public float speed = 10;
    MagicController mana;
    public Image bar;

    void Awake()
    {

        bar = GetComponent<Image>();

    }

    void Update()
    {

        

        bar.fillAmount = Mathf.Lerp(bar.fillAmount, mana.manaPct, Time.deltaTime * speed);
    
    }
}
