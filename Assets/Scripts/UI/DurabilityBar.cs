using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurabilityBar : MonoBehaviour
{
    [Range(0,1)] public float minFill = 0.3f;
    public float fullX = 300;
    public float emptyX = 111;
    public Image background;
    public Image reveal;
    public Image overlay;
    public Sprite tipSprite;
    public Sprite maskSprite;
    public bool inFileSelectScreen;
    SwordController sword;

    void Start ()
    {
        if (inFileSelectScreen) return;
        sword = PlayerController.player.GetComponentInChildren<SwordController>();
        sword.onDurabilityChanged += PlayerDurabilityChanged;
        UpdateSword(sword.durability / sword.maxDurability);
    }

    void OnDestroy ()
    {
        if (inFileSelectScreen || PlayerController.player == null) return;
        sword = PlayerController.player.GetComponentInChildren<SwordController>();
        sword.onDurabilityChanged -= PlayerDurabilityChanged;
    }

    void PlayerDurabilityChanged(float durability, float prevDurability, float maxDurability)
    {
        if (prevDurability > 0) UpdateSword(durability / maxDurability);
    }

    public void UpdateSword(float durabilityPct)
    {
        reveal.fillAmount = durabilityPct * (1-minFill) + minFill;
        RectTransform t = overlay.GetComponent<RectTransform>();
        if (durabilityPct < 0.01f)
        {
            background.enabled = false;
            overlay.enabled = true;
            overlay.sprite = tipSprite;
            t.anchoredPosition = Vector2.right * emptyX;

        }
        else if (durabilityPct < 0.99f)
        {
            overlay.enabled = true;
            background.enabled = true;
            overlay.sprite = maskSprite;
            float x = Mathf.Lerp(emptyX, fullX, durabilityPct);
            t.anchoredPosition = new Vector2(x, 0);
        }
        else
        {
            overlay.enabled = false;
            background.enabled = true;
        }
    }
}
