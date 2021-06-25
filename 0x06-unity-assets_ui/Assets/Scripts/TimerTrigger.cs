using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{

    void OnTriggerExit()
    {
        if (FindObjectOfType<Timer>().timerStart == false)
            FindObjectOfType<Timer>().timerStart = true;
    }
}