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
    }

    IEnumerator TimePlayedCoroutine ()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(1);
            secondsPlayed++;
        }
    }

    public static void GameOver()
    {
        game.gameOverScreen.Setup(game.points);
    }
}
