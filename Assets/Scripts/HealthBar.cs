using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour 
{
    public Sprite fullHeart;
    public Sprite emptyHeart;
    Image[] heartIcons;

    void Start ()
    {
        heartIcons = GetComponentsInChildren<Image>();
        HealthController h = PlayerController.player.GetComponent<HealthController>();
        h.onHealthChanged += PlayerHealthChanged;
    }

    void PlayerHealthChanged(float health, float prevHealth, float maxHealth)
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            if (i < Mathf.RoundToInt(health)) heartIcons[i].sprite = fullHeart;
            else heartIcons[i].sprite = emptyHeart;
        }
    }
}