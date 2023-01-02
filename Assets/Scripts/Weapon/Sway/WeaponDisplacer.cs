using UnityEngine;

public class WeaponDisplacer : MonoBehaviour
{
    [SerializeField] private float displacementIntensity = 0.2f;
    [SerializeField] private float maxDisplacement = 0.5f;
    [SerializeField] private float smoothDisplacement = 6f;

    private Vector3 originalPosition;
    private Vector2 input;

    private void Awake()
    {
        originalPosition = transform.localPosition;
    }

    public void ReadInput(Vector2 input)
    {
        this.input = input;
    }

    public Vector3 DisplacePosition(Vector3 currentPosition)
    {
        float movementX = Mathf.Clamp(input.x * displacementIntensity, -maxDisplacement, maxDisplacement);
        float movementY = Mathf.Clamp(input.y * displacementIntensity, -maxDisplacement, maxDisplacement);

        Vector3 fullyDisplacedPosition = new Vector3(movementX + originalPosition.x, movementY + originalPosition.y, 0f);
        Vector3 displacedPosition = Vector3.Lerp(currentPosition, fullyDisplacedPosition, smoothDisplacement * Time.deltaTime);

        return displacedPosition;
    }
}
