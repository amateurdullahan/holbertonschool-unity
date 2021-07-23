using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public GameObject Player;
    public GameObject MainCamera;
    public GameObject TimerCanvas;
    public GameObject CutsceneCamera;
    public Animator Animation;
    public bool IsPlaying;

    void Start()
    {
        Animation = GetComponent<Animator>();
    }

    void Update()
    {
        if (Animation.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            IsPlaying = true;
        else
            IsPlaying = false;

        if (!IsPlaying)
        {
            MainCamera.SetActive(true);
            TimerCanvas.SetActive(true);
            Player.GetComponent<PlayerController>().enabled = true;
            CutsceneCamera.SetActive(false);
        }
    }
}
