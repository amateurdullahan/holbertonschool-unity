using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle invert;

    public void Start()
    {
        if (PlayerPrefs.GetInt("Inverted") == 1)
        {
            invert.isOn = true;
        }
        else
        {
            invert.isOn = false;
        }
    }

    public void Back()
    {
        string sceneName = PlayerPrefs.GetString("lastLoadedScene");
        SceneManager.LoadScene(sceneName);
    }

    public void Apply()
    {
        if (invert.isOn)
        {
            PlayerPrefs.SetInt("Inverted", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Inverted", 0);
        }
        string sceneName = PlayerPrefs.GetString("lastLoadedScene");
        SceneManager.LoadScene(sceneName);
    }
}
