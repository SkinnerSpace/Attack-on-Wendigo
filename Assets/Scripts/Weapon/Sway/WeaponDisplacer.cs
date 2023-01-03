using UnityEngine;

public class WeaponDisplacer : MonoBehaviour
{
    private const float VERTICAL_ADJUSTMENT = 5f;
    private const float LAND_ADJUSTMENT = 6f;
    private const float STABILITY_MODIFIER = 0.95f;

    [SerializeField] private float displacementIntensity = 0.2f;
    [SerializeField] private float maxDisplacement = 0.5f;
    [SerializeField] private float smoothDisplacement = 6f;

    private WeaponSwayController controller;
    private Vector3 originalPosition;

    private Vector2 input;

    private void Awake()
    {
        controller = GetComponent<WeaponSwayController>();
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        float inputY = controller.input.y;
        inputY = VerticalTuner.IncreaseVerticalInput(inputY, VERTICAL_ADJUSTMENT, LAND_ADJUSTMENT);

        input = new Vector2(
            controller.input.x,
            inputY);

        input *= WeaponAimController.GetStability(STABILITY_MODIFIER);
    }

    public Vector3 DisplacePosition(Vector3 currentPosition)
    {
        float movementX = Mathf.Clamp(input.x * displacementIntensity, -maxDisplacement, maxDisplacement);
        float movementY = Mathf.Clamp(input.y * displacementIntensity, -maxDisplacement * 2f, maxDisplacement);

        Vector3 fullyDisplacedPosition = new Vector3(movementX + originalPosition.x, movementY + originalPosition.y, originalPosition.z);
        Vector3 displacedPosition = Vector3.Lerp(currentPosition, fullyDisplacedPosition, smoothDisplacement * Time.deltaTime);

        return displacedPosition;
    }
}
