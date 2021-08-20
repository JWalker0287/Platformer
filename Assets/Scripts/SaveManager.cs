using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveManager : MonoBehaviour
{
    [System.Serializable]
    public class SaveState
    {
        public string scene;
        public Vector3 position;
        public float health;
        public float maxHealth;
    }

    [ContextMenu("Save Game")]
    public void Save ()
    {
        SaveState saveState = new SaveState();
        saveState.scene = SceneManager.GetActiveScene().name;
        saveState.position = PlayerController.player.transform.position;
        HealthController health = PlayerController.player.GetComponent<HealthController>();
        saveState.health = health.health;
        saveState.maxHealth = health.maxHealth;

        string path = Path.Combine(Application.persistentDataPath, "save.json");
        string json = JsonUtility.ToJson(saveState);
        File.WriteAllText(path, json);
    }

    [ContextMenu("Load Game")]
    public void Load ()
    {
        string path = Path.Combine(Application.persistentDataPath, "save.json");
        string json = File.ReadAllText(path);
        SaveState saveState = JsonUtility.FromJson<SaveState>(json);
        GameManager.LoadGame(saveState);
    }

}

