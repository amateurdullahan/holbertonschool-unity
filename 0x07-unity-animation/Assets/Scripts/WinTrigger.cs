using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameObject winCanvas;
    public GameObject timerCanvas;
    public Timer timer;

    void OnTriggerEnter()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        timer.timerValueTrigger = false;
        timer.Win();
        timerCanvas.SetActive(false);
        winCanvas.SetActive(true);
    }
}
