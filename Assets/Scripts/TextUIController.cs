using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUIController : MonoBehaviour
{
    public static TextUIController textUI;
    public Text Text;
    public GameObject textObject;
    public float textDelay = 0.1f;

    static Queue<IEnumerator> textQueue = new Queue<IEnumerator>();

    void Awake()
    {
        textObject.SetActive(false);
        if (textUI == null) textUI = this;
    }
    
    void OnEnable()
    {
        StartCoroutine(ProcessQueueCoroutine());
    }

    IEnumerator ProcessQueueCoroutine()
    {
        while(enabled)
        {
            while (textQueue.Count > 0)
            {
                textObject.SetActive(true);
                yield return StartCoroutine(textQueue.Dequeue());
            }
            textObject.SetActive(false);
            yield return null;
        }
    }

    public void DisplayText(string text)
    {
        textQueue.Enqueue(textUI.DisplayTextCoroutine(text));
    }
    IEnumerator DisplayTextCoroutine(string text)
    {
        textObject.SetActive(true);
        Text.text = "";
        for (int i = 0; i < text.Length; i ++)
        {
            Text.text = (Text.text + text[i]);
            yield return new WaitForSeconds(textDelay);
        }
        yield return new WaitForSeconds(5);
        textObject.SetActive(false);
    }

}
