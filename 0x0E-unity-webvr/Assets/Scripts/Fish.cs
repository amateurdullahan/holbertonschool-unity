using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fish : MonoBehaviour
{
    void OnJointBreak(float breakForce) {
        gameObject.GetComponent<ConfigurableJoint>().connectedBody.gameObject.GetComponent<Fishing>().Reel();
    }
}
