using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    
    public static InputManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    [SerializeField] private InputActionAsset inputAction;

    private void OnEnable() {
        inputAction.FindActionMap("Camera").Enable();
    }

    private void OnDisable() {
        inputAction.FindActionMap("Camera").Disable();
    }

    public void DisableCamera() {
        inputAction.FindActionMap("Camera").Disable();
    }
    
    public void EnableCamera() {
        inputAction.FindActionMap("Camera").Enable();
    }

}