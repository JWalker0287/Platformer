using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicBar : MonoBehaviour
{
    public float speed = 10;
    public MagicController magic;
    public Image bar;

    void Awake()
    {

        bar = GetComponent<Image>();

    }

    void Update()
    {

        if (magic == null)
        {

            magic = PlayerController.player.GetComponent<MagicController>();

        }
        

        bar.fillAmount = Mathf.Lerp(bar.fillAmount, magic.manaPct, Time.deltaTime * speed);
    
    }
}
