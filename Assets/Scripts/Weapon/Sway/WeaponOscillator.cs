using UnityEngine;

public class WeaponOscillator : MonoBehaviour, ISpeedObserver
{
    [Header("Settings")]
    [SerializeField] private float sinFrequency = 8f;
    [SerializeField] private float sinMagnitude = 1f;

    [SerializeField] private bool sinX = true;
    [SerializeField] private bool sinY = true;

    private Speedometer speedometer;
    private SinCounter sinCounter = new SinCounter();

    public Vector2 movement { get; private set; }

    public void ReadInput()
    {
        if (speedometer != null) Wave(speedometer.speedMagnitude);
    }

    public void Wave(float movementMagnitude)
    {
        float force = movementMagnitude * sinFrequency;

        if (force > 0f)
        {
            sinCounter.CountTime(force);
            movement = GetSinMovement(force);
        }
    }

    private Vector2 GetSinMovement(float movementMagnitude)
    {
        float sin = (Mathf.Sin(sinCounter.time) * sinMagnitude) * movementMagnitude;

        Vector3 sinMovement = new Vector2(
            sinX ? sin : 0f,
            sinY ? sin : 0f);

        return sinMovement;
    }

    public void ConnectSpeedometer(Speedometer speedometer) => this.speedometer = speedometer;
}
