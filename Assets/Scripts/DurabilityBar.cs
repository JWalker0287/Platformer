using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurabilityBar : MonoBehaviour
{
    [Range(0,1)] public float minFill = 0.3f;
    public float fullX = 300;
    public float emptyX = 111;
    public Image bar;
    public Image tip;
    public Sprite[] tipSprites;
    SwordController sword;

    void Start ()
    {
        sword = PlayerController.player.GetComponent<SwordController>();
        sword.onDurabilityChanged += PlayerDurabilityChanged;
    }

    void PlayerDurabilityChanged(float durability, float prevDurability, float maxDurability)
    {
        float d = durability / maxDurability;
        bar.fillAmount = d * (1-minFill) + minFill;
        if (d < 0.95f)
        {
            tip.enabled = true;
            tip.sprite = tipSprites[Random.Range(0, tipSprites.Length)];
            RectTransform t = tip.GetComponent<RectTransform>();
            float x = Mathf.Lerp(emptyX, fullX, d);
            t.anchoredPosition = new Vector2(x, -4.2f);
        }
        else
        {
            tip.enabled = false;
        }
    }
}
