using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YawVR;
/// <summary>
/// Sets the YawTracker's orientation based on the GameObject's orientation
/// </summary>
public class SimpleOrientationCopy : MonoBehaviour
{
    /*
       This script simply copies this gameObject's rotation, and sends it to the YawTracker
    */
    public Transform parent;
    YawController yawController; // reference to YawController
    public float speed;
    public Quaternion calculatedRot;
    private void Start() {
        yawController = YawController.Instance();
    }
    private void FixedUpdate() {
        Quaternion currRot = parent.rotation;
        
        calculatedRot = Quaternion.Slerp(calculatedRot, currRot, speed * Time.deltaTime);
        yawController.TrackerObject.SetRotation(calculatedRot.eulerAngles);
    }
}
