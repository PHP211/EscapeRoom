using Unity.Cinemachine;
using UnityEngine;

public class InterestPoint : MonoBehaviour {
    
    public enum InterestType {
        Puzzle,
        Orbit
    }

    [SerializeField] public CinemachineCamera linkedCamera;
    
    [SerializeField] public InterestType type = InterestType.Puzzle;
}