using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Paraphernalia.Components;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader loader;
    
    public Image transitionImage;
        
    void Awake ()
    {
        if (loader == null) 
        {
            loader = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }    

    public static void LoadScene (string scene)
    {
        loader.StartCoroutine(loader.LoadSceneCoroutine(scene));
    }

    public static void ReloadLevel ()
    {
        loader.StartCoroutine(loader.ReloadLevelCoroutine());
    }

    public static void OpenDoor(int doorID, string scene)
    {
        loader.StartCoroutine(loader.OpenDoorCoroutine(doorID, scene));
    }

    public static void NewGame ()
    {
        loader.StartCoroutine(loader.NewGameCoroutine());
    }

    public static void LoadGame (SaveState saveState)
    {
        loader.StartCoroutine(loader.LoadGameCoroutine(saveState));
    }

    IEnumerator StartTransitionCoroutine (string scene)
    {
        Time.timeScale = 0;
        transitionImage.enabled = true;
        yield return StartCoroutine(Fade(Color.clear, Color.black));

        if (scene == SceneManager.GetActiveScene().name)
        {
            SceneManager.LoadScene(scene);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        else 
        {
            yield return SceneManager.LoadSceneAsync(scene);
        }
    }

    IEnumerator FinishTransitionCoroutine()
    {
        yield return new WaitForEndOfFrame();
        yield return StartCoroutine(Fade(Color.black, Color.clear));
        transitionImage.enabled = false;
        Time.timeScale = 1;
    }

    IEnumerator LoadSceneCoroutine (string scene)
    {
        yield return StartCoroutine(StartTransitionCoroutine(scene));
        yield return StartCoroutine(FinishTransitionCoroutine());
    }

    IEnumerator ReloadLevelCoroutine ()
    {
        yield return StartCoroutine(StartTransitionCoroutine(SceneManager.GetActiveScene().name));

        PlayerController.player.transform.position = PlayerController.player.defaultPosition;
        CameraController.instance.SetPosition();
        
        yield return StartCoroutine(FinishTransitionCoroutine());
    }

    IEnumerator OpenDoorCoroutine (int doorID, string scene)
    {
        yield return StartCoroutine(StartTransitionCoroutine(scene));

        DoorController[] doors = GameObject.FindObjectsOfType<DoorController>();
        foreach (DoorController door in doors)
        {
            if (door.doorID == doorID)
            {
                PlayerController.player.transform.position = door.transform.position;
                CameraController.instance.SetPosition();
                break;
            }
        }

        yield return StartCoroutine(FinishTransitionCoroutine());
    }

    IEnumerator NewGameCoroutine ()
    {
        yield return StartCoroutine(LoadSceneCoroutine("Game"));
        GameManager.secondsPlayed = 0;
        yield return new WaitForEndOfFrame();
        SaveManager.Save();
    }

    IEnumerator LoadGameCoroutine (SaveState saveState)
    {
        yield return StartCoroutine(StartTransitionCoroutine(saveState.scene));

        PlayerController.player.transform.position = saveState.position;
        CameraController.instance.SetPosition();

        HealthController health = PlayerController.player.GetComponent<HealthController>();
        health.maxHealth = saveState.maxHealth;
        health.health = saveState.health;

        MagicController magic = PlayerController.player.GetComponent<MagicController>();
        magic.maxMana = saveState.maxMana;
        magic.mana = saveState.mana;

        SwordController sword = PlayerController.player.GetComponent<SwordController>();
        sword.maxDurability = saveState.maxDurability;
        sword.durability = saveState.durability;

        GameManager.secondsPlayed = saveState.seconds;
        
        yield return StartCoroutine(FinishTransitionCoroutine());
    }

    IEnumerator Fade (Color a, Color b)
    {
        float duration = 0.3f;
        for (float t = 0; t < duration; t += Time.unscaledDeltaTime)
        {
            float frac = t / duration;
            transitionImage.color = Color.Lerp(a, b, frac);
            yield return new WaitForEndOfFrame();
        }
    }
}
