using UnityEngine;

public class WeaponDisplacer : MonoBehaviour
{
    private const float VERTICAL_ADJUSTMENT = 0.2f;
    private const float STABILITY_MODIFIER = 0.95f;

    [SerializeField] private float displacementIntensity = 0.2f;
    [SerializeField] private float maxDisplacement = 0.5f;
    [SerializeField] private float smoothDisplacement = 6f;

    private VerticalTuner verticalTuner;
    private SwayController controller;
    private IAimController aimController;
    private Vector3 originalPosition;

    private Vector2 input;

    private void Awake()
    {
        controller = GetComponent<SwayController>();
        originalPosition = transform.localPosition;
    }

    private void Start()
    {
        aimController = controller.aimController;
    }

    public WeaponDisplacer InitializeOnTake(VerticalTuner verticalTuner)
    {
        this.verticalTuner = verticalTuner;
        return this;
    }

    public void ReadInput()
    {
        float inputY = controller.input.y;

        inputY = verticalTuner.IncreaseVerticalInput(inputY, VERTICAL_ADJUSTMENT);

        input = new Vector2(
            controller.input.x,
            inputY) * 0.5f;

        if (aimController != null){
            input *= aimController.GetStability(STABILITY_MODIFIER);
        }
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
