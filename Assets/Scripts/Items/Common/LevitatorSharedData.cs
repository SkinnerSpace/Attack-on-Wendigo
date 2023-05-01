using UnityEngine;

[CreateAssetMenu(fileName ="LevitatorSharedData", menuName ="ScriptableObjects/Weapon/LeviatorSharedData")]
public class LevitatorSharedData : ScriptableObject
{
    public float frequency = 5f;
    public float magnitude = 0.1f;
    public float pushSpeed = 5f;
    public float rotationSpeed = 50f;
}