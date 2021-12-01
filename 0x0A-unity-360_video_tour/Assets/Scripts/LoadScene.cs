using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadCantina()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadCube()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLivingRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadMezzanine()
    {
        SceneManager.LoadScene(3);
    }
}
