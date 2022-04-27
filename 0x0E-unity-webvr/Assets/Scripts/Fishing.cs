using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fishing : MonoBehaviour
{
    public Text debugText;
    public int biteChance;
    public float biteWindow = 2f;
    public LineRenderer line;
    public float reelForce = 1f;
    public GameObject lure;
    public GameObject fishPrefab;
    public GameObject caught = null;
    
    private Vector3 castPoint = Vector3.zero;
    private RaycastHit hit;
    private Vector3 lastTipPosition;
    private bool fishing = false;
    private float biteTime = 0f;
    private float fishingTime = 0f;

    // Update is called once per frame
    void Update() {
        if (caught != null) {
            castPoint = caught.transform.TransformPoint(new Vector3(0f, 0.5f, 0f));
        }

        line.SetPositions(new Vector3[] {transform.TransformPoint(new Vector3(0f, 0.48f, 0f)), castPoint});

        if (fishing) {
            if (fishingTime % 1f + Time.deltaTime % 1f >= 1f && Random.Range(0, biteChance) == 0) {
                Bite();
            }

            fishingTime += Time.deltaTime;

            if (biteTime > 0f) {
                biteTime -= Time.deltaTime;
            }
        }

        debugText.text = $"";
    }

    void LateUpdate() {
        if (castPoint == Vector3.zero) {
            return;
        }

        if ((lastTipPosition - line.GetPosition(1)).magnitude + reelForce < (line.GetPosition(0) - line.GetPosition(1)).magnitude && caught == null) {
            Reel();
        }

        lastTipPosition = line.GetPosition(0);
    }

    public void Cast() {
        if (castPoint != Vector3.zero) {
            return;
        }

        if (Physics.Raycast(line.GetPosition(0), Vector3.down, out hit, 10f)) {
            line.positionCount = 2;
            castPoint = hit.point;
            line.SetPosition(1, castPoint);
            lure.SetActive(true);
            lure.transform.position = castPoint;

            if (hit.collider.gameObject.layer == 4) {
                fishing = true;
            }
        }
    }

    public void Reel() {
        if (castPoint == Vector3.zero) {
            return;
        }

        lure.SetActive(false);
        if (biteTime > 0f) {
            Catch();
        } else {
            castPoint = Vector3.zero;
            line.positionCount = 1;
            fishing = false;
            caught = null;
        }
    }

    private void Bite() {
        biteTime = biteWindow;
        castPoint = castPoint - new Vector3(0f, 0.1f, 0f);
        lure.transform.position = castPoint;
        line.SetPosition(1, castPoint);
    }

    private void Catch() {
        biteTime = 0f;
        castPoint = line.GetPosition(0) - new Vector3(0f, 0.3f, 0f);
        line.SetPosition(1, castPoint);
        caught = Object.Instantiate(fishPrefab, castPoint, Quaternion.identity);
        caught.GetComponent<ConfigurableJoint>().connectedBody = gameObject.GetComponent<Rigidbody>();
        caught.GetComponent<ConfigurableJoint>().anchor = caught.transform.InverseTransformPoint(line.GetPosition(0));
        caught.GetComponent<ConfigurableJoint>().anchor = transform.InverseTransformPoint(line.GetPosition(0));
        fishing = false;
    }
}
