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
    public bool inFileSelectScreen;
    SwordController sword;

    void Start ()
    {
        if (inFileSelectScreen) return;
        sword = PlayerController.player.GetComponent<SwordController>();
        sword.onDurabilityChanged += PlayerDurabilityChanged;
        UpdateSword(sword.durability / sword.maxDurability);
    }

    void PlayerDurabilityChanged(float durability, float prevDurability, float maxDurability)
    {
        UpdateSword(durability / maxDurability);
    }

    public void UpdateSword(float durabilityPct)
    {
        bar.fillAmount = durabilityPct * (1-minFill) + minFill;
        if (durabilityPct < 0.95f)
        {
            tip.enabled = true;
            tip.sprite = tipSprites[Random.Range(0, tipSprites.Length)];
            RectTransform t = tip.GetComponent<RectTransform>();
            float x = Mathf.Lerp(emptyX, fullX, durabilityPct);
            t.anchoredPosition = new Vector2(x, 0);
        }
        else
        {
            tip.enabled = false;
        }
    }
}
