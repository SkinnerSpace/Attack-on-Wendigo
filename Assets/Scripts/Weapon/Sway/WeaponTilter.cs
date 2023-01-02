using UnityEngine;

public class WeaponTilter : MonoBehaviour
{
    private const float X_MULTIPLIER = 2f;

    [SerializeField] private float tiltIntensity = 4f;
    [SerializeField] private float maxTilt = 5f;
    [SerializeField] private float smoothTilt = 12f;

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

    public Quaternion TiltRotation(Quaternion currentRotation)
    {
        float tiltY = Mathf.Clamp(input.x * tiltIntensity, -maxTilt, maxTilt);
        float tiltX = Mathf.Clamp(input.y * tiltIntensity, -maxTilt, maxTilt) * X_MULTIPLIER;

        Quaternion fullyTiltedRotation = Quaternion.Euler(new Vector3(tiltX, tiltY, tiltY));
        Quaternion tiltedRotation = Quaternion.Slerp(currentRotation, fullyTiltedRotation * originalRotation, smoothTilt * Time.deltaTime);

        return tiltedRotation;
    }
}
