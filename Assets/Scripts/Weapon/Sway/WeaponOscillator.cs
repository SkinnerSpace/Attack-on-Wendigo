using UnityEngine;

public class WeaponOscillator : MonoBehaviour
{
    private const float MAX_SIN_TIME = Mathf.PI * 2f;

    [SerializeField] private float sinFrequency = 8f;
    [SerializeField] private float sinMagnitude = 1f;

    [SerializeField] private bool sinX = true;
    [SerializeField] private bool sinY = true;

    private float sinTime;
    public Vector2 movement { get; private set; }

    private void Update()
    {
        Wave(PlayerHorizontalMovement.velocityMagnitude);
    }

    public void Wave(float movementMagnitude)
    {
        if (movementMagnitude > 0f)
        {
            CountTime(movementMagnitude);
            movement = GetSinMovement(movementMagnitude);
        }
    }

    private void CountTime(float movementFrequency)
    {
        sinTime += (sinFrequency * movementFrequency) * Time.deltaTime;

        if (sinTime > MAX_SIN_TIME)
            sinTime -= MAX_SIN_TIME;
    }

    private Vector2 GetSinMovement(float movementMagnitude)
    {
        float sin = (Mathf.Sin(sinTime) * sinMagnitude) * movementMagnitude;

        Vector3 sinMovement = new Vector2(
            sinX ? sin : 0f,
            sinY ? sin : 0f);

        return sinMovement;
    }
}
