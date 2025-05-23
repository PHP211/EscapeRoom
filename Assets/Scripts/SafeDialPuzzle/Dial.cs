using System;
using UnityEngine;

public class Dial : MonoBehaviour {

    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private LayerMask interactableLayer;
    public bool isLocked = false;
    
    private bool isDragging = false;
    private float currentAngle = 0f;
    private Vector3 lastMousePos;
    
    public float CurrentAngle => currentAngle;

    private void Update() {
        HandleDial();
    }

    private void HandleDial() {
        if (isLocked) return;

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, interactableLayer)) {
                if (hit.transform == transform) {
                    lastMousePos = Input.mousePosition;
                    isDragging = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            isDragging = false;
            InputManager.Instance.EnableCamera();
        }

        if (isDragging) {
            InputManager.Instance.DisableCamera();
            
            Vector2 delta = Input.mousePosition - lastMousePos;
            float angle = delta.x + rotationSpeed * Time.deltaTime;
            
            currentAngle += angle;
            currentAngle %= 360;
            
            transform.localEulerAngles = new Vector3(0, -currentAngle, -90);
            
            lastMousePos = Input.mousePosition;
        }
    }

    public void LockDial() {
        isLocked = true;
    }

    public void Reset() {
        isLocked = false;
        currentAngle = 0f;
        transform.localEulerAngles = new Vector3(0, 0, -90);
    }

}
