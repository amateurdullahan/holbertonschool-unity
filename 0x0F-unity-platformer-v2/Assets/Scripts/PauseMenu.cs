using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;

    public void Pause()
    {
        gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.visible = false;
    }

    public void Restart()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.visible = false;
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void MainMenu()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene(0);
    }

    public void Options()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        PlayerPrefs.SetString("lastLoadedScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(4);
    }
}
