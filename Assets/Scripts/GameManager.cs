using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Paraphernalia.Components;

public class GameManager : MonoBehaviour
{
    public Image transitionImage;
    public static GameManager game;
    void Awake ()
    {
        if (game == null) 
        {
            game = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }
    public static void OpenDoor(int doorID, string scene)
    {
        game.StartCoroutine(game.OpenDoorCoroutine(doorID, scene));
    }

    IEnumerator OpenDoorCoroutine (int doorID, string scene)
    {
        Time.timeScale = 0;
        transitionImage.enabled = true;
        yield return StartCoroutine(Fade(Color.clear, Color.white));


        if (scene == SceneManager.GetActiveScene().name)
        {
            SceneManager.LoadScene(scene);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        else 
        {
            yield return SceneManager.LoadSceneAsync(scene);
        }

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

        yield return StartCoroutine(Fade(Color.white,Color.clear));
        transitionImage.enabled = false;
        Time.timeScale = 1;
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
