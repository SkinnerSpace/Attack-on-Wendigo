using UnityEngine;

public class WeaponTilter : MonoBehaviour
{
    private const float HORIZONTAL_TILT_MULTIPLIER = 2f;
    private const float STABILITY_MODIFIER = 0.7f;

    [SerializeField] private float tiltIntensity = 4f;
    [SerializeField] private float maxTilt = 5f;
    [SerializeField] private float smoothTilt = 12f;

    private SwayController controller;
    private IAimController aimController;
    private Quaternion originalRotation;

    private Vector2 input;

    private void Awake()
    {
        controller = GetComponent<SwayController>();
        originalRotation = transform.localRotation;
    }

    private void Start()
    {
        aimController = controller.aimController;
    }

    public void ReadInput()
    {
        float inputY = controller.input.y;

        input = new Vector2(
            controller.input.x * 0.2f, 
            inputY * 0.1f);

        if (aimController != null){
            input *= aimController.GetStability(STABILITY_MODIFIER);
        }
    }

    public Quaternion TiltRotation(Quaternion currentRotation)
    {
        float tiltY = Mathf.Clamp(input.x * tiltIntensity, -maxTilt, maxTilt);
        float tiltX = Mathf.Clamp(input.y * tiltIntensity, -maxTilt, maxTilt) * HORIZONTAL_TILT_MULTIPLIER;

        Quaternion fullyTiltedRotation = Quaternion.Euler(new Vector3(tiltX, tiltY, tiltY));
        Quaternion tiltedRotation = Quaternion.Slerp(currentRotation, fullyTiltedRotation * originalRotation, smoothTilt * Time.deltaTime);

        return tiltedRotation;
    }
}
