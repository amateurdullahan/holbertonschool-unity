using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoTour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwitchCantina()
    {
        SceneManager.LoadScene("Cantina");
    }

    void SwitchCube()
    {
        SceneManager.LoadScene("Cube");
    }

    void SwitchLivingRoom()
    {
        SceneManager.LoadScene("LivingRoom");
    }

    void SwitchMezzanine()
    {
        SceneManager.LoadScene("Mezzanine");
    }
}
