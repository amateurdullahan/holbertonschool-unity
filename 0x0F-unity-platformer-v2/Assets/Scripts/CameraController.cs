using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform lookAt;
    public Transform camTransform;
    private Camera cam;
    public bool isInverted;
    public GameObject winCanvas;
    public GameObject pauseCanvas;

    private float distance = 6.25f;
    private float currentX = 0f;
    private float currentY = 0f;
    private float sensitivity = 3f;

    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    
        camTransform = transform;
        cam = Camera.main;

        if (PlayerPrefs.GetInt("Inverted") == 1)
        {
            isInverted = true;
        }
        else
        {
            isInverted = false;
        }
    }

    void Update()
    {
        if (winCanvas.activeSelf == false && pauseCanvas.activeSelf == false)
        {
            currentX += Input.GetAxisRaw("Mouse X");
            if (isInverted)
            {
                currentY -= Input.GetAxisRaw("Mouse Y");
            }
            else
            {
                currentY += Input.GetAxisRaw("Mouse Y");
            }
            currentY = Mathf.Clamp(currentY, -25f, 25f);
        }
    }

    void LateUpdate()
    {
        Vector3 direction = new Vector3 (0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY * sensitivity, currentX * sensitivity, 0);

        camTransform.position = lookAt.position + rotation * direction;
        camTransform.LookAt(lookAt.position);
    }

}
