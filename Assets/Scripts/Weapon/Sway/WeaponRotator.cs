using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    [SerializeField] private float rotationIntensity = 3f;
    [SerializeField] private float smoothRotation = 5f;

    private Quaternion originalRotation;
    private Vector2 input;

    private void Awake()
    {
        originalRotation = transform.localRotation;
    }

    public void ReadInput(Vector2 input)
    {
        this.input = input;
    }

    public Quaternion OffsetRotation(Quaternion currentRotation)
    {
        Quaternion horizontalAdjustment = Quaternion.AngleAxis(-rotationIntensity * input.x, Vector3.up);
        Quaternion verticalAdjustment = Quaternion.AngleAxis(rotationIntensity * input.y, Vector3.right);

        Quaternion fullyOffsettedRotation = originalRotation * horizontalAdjustment * verticalAdjustment;
        Quaternion rotation = Quaternion.Lerp(currentRotation, fullyOffsettedRotation, smoothRotation * Time.deltaTime);

        return rotation;
    }
}
