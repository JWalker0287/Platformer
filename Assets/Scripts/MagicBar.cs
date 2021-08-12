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
    MagicController magic;

    void Start ()
    {
        magic = PlayerController.player.GetComponent<MagicController>();
    }

    void Update()
    {
        RectTransform t = bar.GetComponent<RectTransform>();
        float m = magic.mana / magic.maxMana;
        float x = Mathf.Sin(Time.time * m)* width;
        float y = Mathf.Lerp(emptyY, fullY, m);
        t.anchoredPosition = new Vector2(x, y);
    }
}
