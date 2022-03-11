using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimerText;
    public Text EndText;
    public float timerValue = 0f;
    public bool timerValueTrigger = false;

    // Update is called once per frame
    void Update()
    {
        if (timerValueTrigger)
        {
            timerValue += Time.deltaTime;
            float minutes = Mathf.FloorToInt(timerValue / 60);
            float seconds = Mathf.FloorToInt(timerValue % 60);
            float milliseconds = (timerValue % 1) * 100;
            TimerText.text = string.Format("{0}:{1:00}.{2:00}", minutes, seconds, milliseconds);
        }
    }

    public void Win()
    {
        EndText.text = TimerText.text;
    }
}
