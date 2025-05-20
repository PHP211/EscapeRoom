using System;
using UnityEngine;
using System.Collections.Generic;

public class SafeDial : MonoBehaviour {

    [SerializeField] private Dial dial;
    [SerializeField] private Transform indicator;
    [SerializeField] private List<Transform> markers = new List<Transform>();
    
    [SerializeField] private List<int> unlockSequence = new List<int> { 3, 2, 1, 0, 5, 6, 7, 8, 9, 4 };
    [SerializeField] private List<string> directions = new List<string> { "L", "L", "L", "L", "R", "R", "R", "R", "L", "L" };
    
    private int currentStep = 0;
    private float lastAngle = 0f;
    // private float directionThreshold = 10f;
    
    private List<Renderer> markerRenderers = new List<Renderer>();
    private List<Color> originalColors = new List<Color>();

    private void Start() {
        foreach (Transform marker in markers) {
            Renderer r = marker.GetComponentInChildren<Renderer>();
            if (r != null) {
                markerRenderers.Add(r);
                originalColors.Add(r.material.color); // lưu lại màu ban đầu
            }
        }
    }

    private void Update() {

        if (dial.isLocked) return;
        
        Debug.Log(markerRenderers.Count);

        float currentAngle = dial.CurrentAngle;
        float delta = Mathf.DeltaAngle(lastAngle, currentAngle);
        lastAngle = currentAngle;

        int markerIndex = GetClosestMarkerIndex();
        string moveDirection = delta > 0 ? "R" : "L";
        
        if (markerIndex == unlockSequence[currentStep] && moveDirection == directions[currentStep]) {
            if (markerIndex >= 0 && markerIndex < markerRenderers.Count) {
                markerRenderers[markerIndex].material.color = Color.green;
            }
            
            currentStep++;

            if (currentStep >= unlockSequence.Count) {
                dial.LockDial();
                OnPuzzleComplete();
            }
        }
        else if (markerIndex == unlockSequence[currentStep] && moveDirection != directions[currentStep]) {
            ResetPuzzle();
        }

    }

    private int GetClosestMarkerIndex() {
        Vector3 indicatorPosition = indicator.position;
        float closestDistance = float.MaxValue;
        int closestMarkerIndex = -1;

        for (int i = 0; i < markers.Count; i++) {
            float distance = Vector3.Distance(indicatorPosition, markers[i].position);
            if (distance < closestDistance) {
                closestDistance = distance;
                closestMarkerIndex = i;
            }
        }
        Debug.Log(closestMarkerIndex);
        return closestMarkerIndex;
    }
    
    void ResetPuzzle() {
        currentStep = 0;
        dial.Reset();
        
        for (int i = 0; i < markerRenderers.Count; i++) {
            markerRenderers[i].material.color = originalColors[i];
        }
    }

    void OnPuzzleComplete() {
        Debug.Log("Unlocked");
        InputManager.Instance.EnableCamera();
    }

}