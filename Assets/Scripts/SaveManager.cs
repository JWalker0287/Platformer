using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[System.Serializable]
public class SaveState
{
    public string scene;
    public Vector3 position;
    public float health;
    public float maxHealth;
    public float mana;
    public float maxMana;
    public float durability;
    public float maxDurability;
    public int seconds;

    public override string ToString ()
    {
        return scene + ", " + position;
    }
}

public class SaveManager : MonoBehaviour
{
    public static int saveIndex;

    public static void Save ()
    {
        SaveState saveState = new SaveState();

        saveState.scene = SceneManager.GetActiveScene().name;
        saveState.position = PlayerController.player.transform.position;
        saveState.seconds = GameManager.secondsPlayed;

        HealthController health = PlayerController.player.GetComponent<HealthController>();
        saveState.health = health.health;
        saveState.maxHealth = health.maxHealth;

        MagicController magic = PlayerController.player.GetComponent<MagicController>();
        saveState.mana = magic.mana;
        saveState.maxMana = magic.maxMana;

        SwordController sword = PlayerController.player.GetComponentInChildren<SwordController>();
        saveState.durability = sword.durability;
        saveState.maxDurability = sword.maxDurability;

        string path = Path.Combine(Application.persistentDataPath, "save"+saveIndex+".json");
        string json = JsonUtility.ToJson(saveState);
        File.WriteAllText(path, json);
    }

    public static SaveState Get (int index)
    {
        string path = Path.Combine(Application.persistentDataPath, "save"+index+".json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveState saveState = JsonUtility.FromJson<SaveState>(json);
            return saveState;
        }
        return null;
    }

    public static void Delete (int index)
    {
        string path = Path.Combine(Application.persistentDataPath, "save"+index+".json");
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public static void Load (int index)
    {
        saveIndex = index;
        string path = Path.Combine(Application.persistentDataPath, "save"+index+".json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveState saveState = JsonUtility.FromJson<SaveState>(json);
            SceneLoader.LoadGame(saveState);
        }
        else SceneLoader.NewGame();
    }

}
