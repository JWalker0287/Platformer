using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadTextFromFile : MonoBehaviour
{
    public string fileName = "credits.txt";

    [ContextMenu("Load File")]
    void LoadTextFile ()
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);
        GetComponent<Text>().text = File.ReadAllText(path);
    }

    void Start()
    {
        LoadTextFile();
    }
}
