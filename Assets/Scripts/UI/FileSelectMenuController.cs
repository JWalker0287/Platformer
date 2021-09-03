using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileSelectMenuController : MonoBehaviour
{
    
    public GameObject fileSelectButtonsRoot;
    private FileSelectButtonController[] fileSelectButtons;

    public GameObject clearButtonsRoot;
    private Button[] clearButtons;

    void Start ()
    {
        fileSelectButtons = fileSelectButtonsRoot.GetComponentsInChildren<FileSelectButtonController>();
        clearButtons = clearButtonsRoot.GetComponentsInChildren<Button>();

        if (fileSelectButtons.Length != clearButtons.Length)
        {
            Debug.LogError("Number of file buttons and clear buttons is different.");
        }
        UpdateUI();
    }

    void UpdateUI ()
    {
        for (int i = 0; i < clearButtons.Length; i++)
        {
            SaveState save = SaveManager.Get(i);
            clearButtons[i].interactable = (save != null);
            fileSelectButtons[i].Setup(save);
        }
    }

    public void Clear (int index)
    {
        SaveManager.Delete(index);
        UpdateUI();
    }

}
