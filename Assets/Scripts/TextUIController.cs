using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUIController : MonoBehaviour
{

    public Text textBox;

    void Awake()
    {

        textBox.enabled = false;

    }

    void Update()
    {

        if (Input.GetButton("Fire1") && textBox.enabled == true)
        {

            ReadSign();

        }

    }

    public void SignInteraction()
    {

        textBox.enabled = true;

        textBox.text = "Press Swing to read.";

    }

    void ReadSign()
    {

        textBox.text = "Wow you can read! Congratulations!!!!";

    }

    


}
