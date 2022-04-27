using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatControls : MonoBehaviour
{
    public float sensitivity = 100f;
    public Vector3 heldItemPosition;
    public Vector3 heldItemRotation;

    private float xRotation = 0f;
    private float yRotation = 0f;
    private bool mouse = false;
    private GameObject heldItem;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (!mouse) {
                mouse = true;
                Cursor.lockState = CursorLockMode.Locked;
            } else {
                RaycastHit hit;
                if (Physics.Raycast(gameObject.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out hit) && hit.collider.gameObject.tag == "Interactable") {
                    heldItem = hit.collider.gameObject;
                    hit.transform.parent = transform;
                    hit.transform.localPosition = heldItemPosition;
                    hit.transform.localRotation = Quaternion.Euler(heldItemRotation);
                    heldItem.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && heldItem) {
            GameObject fish = heldItem.GetComponent<Fishing>().caught;

            if (fish) {
                fish.GetComponent<ConfigurableJoint>().breakForce = 0.001f;
            }

            heldItem.GetComponent<Interactable>().onTrigger.Invoke();
        }

        if (mouse) {
            float x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            xRotation -= y;
            yRotation += x;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
            transform.position = new Vector3(0f, 0.81f, 0f);
        }
    }
}
