using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TargetController : MonoBehaviour
{
    public float speed = 1.0f;

    [SerializeField]
    private float threshold = 0.1f;
    private Vector3 dest;
    private ARPlane plane;

    void Start() {
        plane = transform.parent.GetComponent<ARPlane>();
        dest = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(dest, transform.localPosition) < threshold) {
            int r = Random.Range(0, plane.boundary.Length - 1);
            dest = new Vector3(plane.boundary[r].x, 0, plane.boundary[r].y);
        }

        transform.localPosition += new Vector3(speed * Time.deltaTime * (dest.x - transform.localPosition.x), 0, speed * Time.deltaTime * (dest.z - transform.localPosition.z));
    }
}
