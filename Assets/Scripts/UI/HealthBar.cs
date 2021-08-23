using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour 
{
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public bool inFileSelectScreen;
    Image[] heartIcons;

    void Start ()
    {
        heartIcons = GetComponentsInChildren<Image>();
        if (inFileSelectScreen) return;

        HealthController h = PlayerController.player.GetComponent<HealthController>();
        h.onHealthChanged += PlayerHealthChanged;
        UpdateHearts(Mathf.RoundToInt(h.health));
    }

    void PlayerHealthChanged(float health, float prevHealth, float maxHealth)
    {
        UpdateHearts(Mathf.RoundToInt(health));
    }

    public void UpdateHearts (int hearts)
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            if (i < hearts) heartIcons[i].sprite = fullHeart;
            else heartIcons[i].sprite = emptyHeart;
        }
    }
}