using System;
using UnityEngine;
// ReSharper disable All

public class MainCameraController : MonoBehaviour {
    
    [SerializeField] private float sensitivity = 0.5f;
    [SerializeField] private float verticalClampMin = -60f;
    [SerializeField] private float verticalClampMax = 60f;
    [SerializeField] private float smoothSpeed = 5f;
    
    private float targetYaw;
    private float targetPitch;

    private float currentYaw;
    private float currentPitch;

    void Start() {
        Vector3 euler = transform.eulerAngles;
        targetYaw = currentYaw = euler.y;
        targetPitch = currentPitch = euler.x;
    }

    void Update() {
        if (Input.GetMouseButton(0)) {
            targetYaw -= Input.GetAxis("Mouse X") * sensitivity;
            targetPitch += Input.GetAxis("Mouse Y") * sensitivity;
            targetPitch = Mathf.Clamp(targetPitch, verticalClampMin, verticalClampMax);
        }

        currentYaw = Mathf.LerpAngle(currentYaw, targetYaw, Time.deltaTime * smoothSpeed);
        currentPitch = Mathf.LerpAngle(currentPitch, targetPitch, Time.deltaTime * smoothSpeed);

        transform.rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
    }
    
}