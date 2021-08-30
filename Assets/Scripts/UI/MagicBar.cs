using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicBar : MonoBehaviour
{
    public float fullY = -18;
    public float emptyY = -169;
    public float width = 40;
    public Image bar;
    public bool inFileSelectScreen;
    MagicController magic;

    void Start ()
    {
        if (inFileSelectScreen) return;
        magic = PlayerController.player.GetComponent<MagicController>();
        UpdateMagicMeter(magic.mana / magic.maxMana);
    }

    void Update()
    {
        if (inFileSelectScreen) return;
        UpdateMagicMeter(magic.mana / magic.maxMana);
    }

    public void UpdateMagicMeter (float magicPercent)
    {
        RectTransform t = bar.GetComponent<RectTransform>();
        float x = Mathf.Sin(Time.time * magicPercent)* width;
        float y = Mathf.Lerp(emptyY, fullY, magicPercent);
        t.anchoredPosition = new Vector2(x, y);
    }
}
