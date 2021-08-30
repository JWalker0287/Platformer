using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileSelectButtonController : MonoBehaviour
{
    public Text newGameText;
    public Text timePlayedText;
    public Text sceneNameText;
    public GameObject playerStatsRoot;

    public void Setup (SaveState save)
    {
        bool saveExists = (save != null);
        newGameText.gameObject.SetActive(!saveExists);
        playerStatsRoot.SetActive(saveExists);
        timePlayedText.gameObject.SetActive(saveExists);
        sceneNameText.gameObject.SetActive(saveExists);

        if (saveExists)
        {
            sceneNameText.text = save.scene;

            string s = "";
            int hours = save.seconds / 3600;
            if (hours > 0) s += hours.ToString() + "h ";
            int minutes = (save.seconds / 60) % 60;
            s += minutes.ToString() + "m";
            timePlayedText.text = s;

            HealthBar health = playerStatsRoot.GetComponentInChildren<HealthBar>();
            health.UpdateHearts(Mathf.RoundToInt(save.health));

            MagicBar magic = playerStatsRoot.GetComponentInChildren<MagicBar>();
            magic.UpdateMagicMeter(save.mana / save.maxMana);

            DurabilityBar sword = playerStatsRoot.GetComponentInChildren<DurabilityBar>();
            sword.UpdateSword(save.durability / save.maxDurability);
        }
    }

    public void LoadGame ()
    {
        int index = transform.GetSiblingIndex();
        SaveManager.Load(index);
    }
}
