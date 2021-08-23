using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
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
