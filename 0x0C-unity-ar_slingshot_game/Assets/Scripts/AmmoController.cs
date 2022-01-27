using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AmmoController : MonoBehaviour
{
    public Vector3 offset = new Vector3(0f, 0f, 0.3f);
    public float strength = 110f;
    public float lineResolution = 0.25f;
    public float maxCurveLength = 6f;

    private Camera cam;
    private ARPlane plane;
    private Rigidbody rigbod;
    private GameLogic gamlog;
    private LineRenderer linren;
    private bool touching = false;
    private Vector3 startPos;
    private Vector3 endPos;
    private List<Vector3> linePoints = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        cam = Object.FindObjectsOfType<Camera>()[0];
        transform.parent = cam.transform;
        transform.localPosition = offset;
        plane = Object.FindObjectsOfType<ARPlane>()[0];
        rigbod = GetComponent<Rigidbody>();
        gamlog = Object.FindObjectsOfType<GameLogic>()[0];
        linren = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < plane.transform.position.y && transform.parent == null) {
            Reset();
        }

        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if (touching == false) {
                startPos = cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, offset.z));
                touching = true;
            }

            endPos = cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, offset.z));
            transform.position = endPos;
            DrawTrajectory((startPos - endPos).normalized * (Vector3.Distance(startPos, endPos) * 21));
        } else {
            if (touching) {
                rigbod.isKinematic = false;
                transform.parent = null;
                rigbod.AddForce((startPos - endPos).normalized * (Vector3.Distance(startPos, endPos) * 10 * strength));
            }
            touching = false;
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (transform.parent == null) {
            if (collision.gameObject.tag == "Target") {
                collision.gameObject.SetActive(false);
                gamlog.score += 10;
            }

            Reset();
        }
    }

    private void Reset() {
        transform.parent = cam.transform;
        transform.localPosition = offset;
        rigbod.isKinematic = true;
        gamlog.ammoCount--;
    }

    private void UpdateTrajectory(Vector3 forceVector) {
        Vector3 velocity = (forceVector / rigbod.mass) * Time.fixedDeltaTime;
        float flightDuration = (2 * velocity.y) / Physics.gravity.y;
        float stepTime = flightDuration / lineResolution;

        linePoints.Clear();

        for (int i = 0; i < lineResolution; i++) {
            float stepTimePassed = stepTime * i;
            Vector3 movementVector = new Vector3(velocity.x * stepTimePassed, velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed, velocity.z * stepTimePassed);

            linePoints.Add(-movementVector + transform.position);
        }

        linren.positionCount = linePoints.Count;
        linren.SetPositions(linePoints.ToArray());
    }

    private void DrawTrajectory(Vector3 startVelocity)
    {
        Vector3 currentPosition = transform.position;
        Vector3 currentVelocity = startVelocity;
        RaycastHit hit;
        Ray ray = new Ray(currentPosition, currentVelocity.normalized);
        float t;

        linePoints.Clear();
        linePoints.Add(transform.position);

        while (!Physics.Raycast(ray, out hit, lineResolution) && Vector3.Distance(transform.position, currentPosition) < maxCurveLength)
        {
            t = lineResolution / currentVelocity.magnitude;
            currentVelocity = currentVelocity + t * Physics.gravity;
            currentPosition = currentPosition + t * currentVelocity;

            linePoints.Add(currentPosition);
            ray = new Ray(currentPosition, currentVelocity.normalized);
        }

        if (hit.transform)
        {
            linePoints.Add(hit.point);
        }

        linren.positionCount = linePoints.Count;
        linren.SetPositions(linePoints.ToArray());
    }
}
