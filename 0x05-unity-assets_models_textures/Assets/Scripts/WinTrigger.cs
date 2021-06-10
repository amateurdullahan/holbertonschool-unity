using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Text timerText;
    void OnTriggerEnter()
    {
        FindObjectOfType<Timer>().timerStart = false;
        timerText.color = Color.green;
        timerText.fontSize = 60;
    }
}