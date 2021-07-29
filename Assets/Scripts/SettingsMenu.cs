using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions;

        List<string> options = new List<string>();

        for(int i = 0; i < Resolution.Length; i++)
        {
            string option = resolutions[i].width + " X " + resolutions[i].height;
            
        }
    }
    public void SetVolume (float volume)
    {

    }
}
