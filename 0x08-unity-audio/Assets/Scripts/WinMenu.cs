using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    public void MainMenu()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Next()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        if (sceneNumber == 3)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(sceneNumber + 1);
        }
    }
}
