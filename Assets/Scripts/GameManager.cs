using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Paraphernalia.Components;

public class GameManager : MonoBehaviour
{
    public static GameManager game;
    public static int secondsPlayed;
    
    public GameOverScreen gameOverScreen;
    
    int points = 0;
    
    void Awake ()
    {
        if (game == null) 
        {
            game = this;
            DontDestroyOnLoad(gameObject);
            gameOverScreen.gameObject.SetActive(false);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    void OnEnable ()
    {
        StartCoroutine(TimePlayedCoroutine());
        HealthController.onAnyDeath += DramaticDeath;
    }

    void OnDisable ()
    {
        HealthController.onAnyDeath -= DramaticDeath;
    }

    void DramaticDeath (HealthController health)
    {
        StartCoroutine(DramaticDeathCoroutine());
    }

    IEnumerator DramaticDeathCoroutine ()
    {
        Time.timeScale = 0.25f;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1;
    }

    IEnumerator TimePlayedCoroutine ()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(1);
            secondsPlayed++;
        }
    }

    public static void ResetGame ()
    {
        game.gameOverScreen.gameObject.SetActive(false);
    }

    public static void GameOver()
    {
        if (game == null) return;
        game.gameOverScreen.Setup(game.points);
    }
}
