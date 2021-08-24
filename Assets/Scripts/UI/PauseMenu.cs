using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    public GameObject pauseRoot;

    public static bool paused {
        get { return instance != null && instance.pauseRoot.activeSelf; }
        set { if (instance != null) instance.pauseRoot.SetActive(value); }
    }

    void Awake ()
    {
        instance = this;
        pauseRoot.SetActive(false);
    }

    void Update ()
    {
        if (Input.GetButtonDown("Cancel")) ShowHide();
    }

    public static void ShowHide ()
    {
        instance.pauseRoot.SetActive(!paused);
        Time.timeScale = paused ? 0 : 1;
        if (paused)
        {
            // select first menu item
        }
    }

    public void Resume ()
    {
        ShowHide();
    }

    public void Save ()
    {
        SaveManager.Save();
    }

    public void Options ()
    {
        //TODO
    }

    public void QuitToMenu ()
    {
        SceneLoader.LoadScene("MainMenu");
    }

    public void QuitToDesktop ()
    {
        Application.Quit();
    }
}
