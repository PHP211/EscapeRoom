using System;
using System.Drawing;
using Unity.Cinemachine;
using UnityEngine;
using Object = System.Object;

public class CameraSwitcher : MonoBehaviour {

    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float doubleClickDelay = 0.2f;
    
    private float lastClickTime;
    
    private CinemachineCamera currentCamera;
    private CinemachineCamera mainCamera;
    private CinemachineCamera puzzleCamera;
    private CinemachineCamera orbitCamera;

    private enum CameraState {
        Main, Puzzle, Orbit
    }
    
    private CameraState currentState = CameraState.Main;

    private void Start() {
        currentCamera = GetTopPrioCamera();
        mainCamera = currentCamera;
        mainCamera.Priority = 20;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Left Click");
            if (Time.time - lastClickTime < doubleClickDelay) {
                HandleDoubleClick();
            }
            lastClickTime = Time.time;
        }

        if (Input.GetMouseButtonDown(1)) {
            Debug.Log("Right Click");
            GoBack();
        }
    }

    // private void HandleSwitchingCamera() {
    //     
    // }

    private void HandleDoubleClick() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, interactableLayer)) {
            InterestPoint interestPoint = hit.collider.GetComponent<InterestPoint>();
            
            if (interestPoint == null || interestPoint.linkedCamera == null) return;

            switch (currentState) {
                case CameraState.Main:
                    if (interestPoint.type == InterestPoint.InterestType.Puzzle) {
                        puzzleCamera = interestPoint.linkedCamera;
                        SwitchTo(puzzleCamera);
                        currentState = CameraState.Puzzle;
                        Debug.Log(currentState);
                    }
                    break;
                
                case CameraState.Puzzle:
                    if (interestPoint.type == InterestPoint.InterestType.Orbit) {
                        orbitCamera = interestPoint.linkedCamera;
                        SwitchTo(orbitCamera);
                        currentState = CameraState.Orbit;
                        Debug.Log(currentState);
                    }
                    break;
                
                default: return;
            }
        }
    }

    private void GoBack() {
        Debug.Log(currentState);
        switch (currentState) {
            case CameraState.Orbit:
                Debug.Log("Switching to puzzle camera");
                SwitchTo(puzzleCamera);
                currentState = CameraState.Puzzle;
                break;
            
            case CameraState.Puzzle:
                Debug.Log("Switching to main camera");
                SwitchTo(mainCamera);
                currentState = CameraState.Main;
                break;
        }
    }

    private void SwitchTo(CinemachineCamera targetCamera) {
        Debug.Log(targetCamera.name);
        Debug.Log(targetCamera.Priority);
        
        if (targetCamera == null || targetCamera == currentCamera) return;
        
        // Reset all priority
        var allCams = FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None);
        foreach (var cam in allCams) {
            cam.Priority = 0;
        }
        
        // Set targetCamera's priority to highest
        currentCamera = targetCamera;
        currentCamera.Priority = 20;
    }

    private CinemachineCamera GetTopPrioCamera() {
        var allCams = FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None);
        CinemachineCamera topCam = null;
        int topPrio = int.MinValue;

        foreach (var cam in allCams) {
            if (cam.Priority > topPrio) {
                topCam = cam;
                topPrio = cam.Priority;
            }
        }
        
        return topCam;
    }
    
}