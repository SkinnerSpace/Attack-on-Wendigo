using UnityEngine;

public class WeaponOscillator : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private PlayerHorizontalMovement horizontalMover;

    [Header("Settings")]
    [SerializeField] private float sinFrequency = 8f;
    [SerializeField] private float sinMagnitude = 1f;

    [SerializeField] private bool sinX = true;
    [SerializeField] private bool sinY = true;

    private SinCounter sinCounter = new SinCounter();

    public Vector2 movement { get; private set; }

    private void Update()
    {
        Wave(horizontalMover.velocityMagnitude * sinFrequency);
    }

    public void Wave(float movementMagnitude)
    {
        if (movementMagnitude > 0f)
        {
            sinCounter.CountTime(movementMagnitude);
            movement = GetSinMovement(movementMagnitude);
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
}
