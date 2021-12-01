using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blurbs : MonoBehaviour
{

    public GameObject BlurbText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Blurb()
    {
        if (BlurbText.activeSelf == false)
        {
            BlurbText.SetActive(true);
        }
        else
        {
            BlurbText.SetActive(false);
        }
    }
}
