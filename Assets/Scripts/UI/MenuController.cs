using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    void Awake ()
    {
        Spawner.DisableAll();
        if (GameManager.game != null) Destroy(GameManager.game.gameObject);
        if (PlayerController.player != null) Destroy(PlayerController.player.gameObject);
    }

    public void LoadScene(string sceneName)
    {
        SceneLoader.LoadScene(sceneName);
    }

    public void QuitGame ()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
