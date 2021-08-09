using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUIController : MonoBehaviour {

    public static TextUIController instance;
    public GameObject panel;
    public Text text;

    static Queue<IEnumerator> textQueue = new Queue<IEnumerator>();

    void Awake ()
    {
        instance = this;
        panel.SetActive(false);
    }

    void OnEnable ()
    {
        StartCoroutine(ProcessQueueCoroutine());
    }

    IEnumerator ProcessQueueCoroutine ()
    {
        while (enabled)
        {
            while (textQueue.Count > 0)
            {
                panel.SetActive(true);
                yield return StartCoroutine(textQueue.Dequeue());
            }
            panel.SetActive(false);
            yield return null;
        }
    }

    public static void ShowText (string text)
    {
        textQueue.Enqueue(instance.ShowTextCoroutine(text));
    }

    IEnumerator ShowTextCoroutine (string text)
    {
        this.text.text = "";
        for (int i = 0; i < text.Length; i++)
        {
            this.text.text += text[i];
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

    public static void WaitText (string text)
    {
        textQueue.Enqueue(instance.WaitTextCoroutine(text));
    }

    IEnumerator WaitTextCoroutine (string text)
    {
        Time.timeScale = 0;
        yield return StartCoroutine(ShowTextCoroutine(text));
        while (Input.GetButton("Submit")) yield return null;
        while (!Input.GetButton("Submit")) yield return null;
        Time.timeScale = 1;
    }
}