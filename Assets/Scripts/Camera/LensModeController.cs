using System;
using UnityEngine;

public class LensModeController : MonoBehaviour {
    
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask normalLayerMask;
    [SerializeField] private LayerMask lensLayerMask;
    
    private bool lensActive = false;

    private void Start() {
        mainCamera.cullingMask = normalLayerMask;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            ToggleLensMode();
        }
    }

    private void ToggleLensMode() {
        lensActive = !lensActive;

        if (lensActive) {
            Debug.Log("Lens Mode");
            mainCamera.cullingMask = lensLayerMask;
        }
        else {
            Debug.Log("Normal Mode");
            mainCamera.cullingMask = normalLayerMask;
        }
    }

}