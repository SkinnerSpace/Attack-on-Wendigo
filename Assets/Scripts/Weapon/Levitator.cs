using UnityEngine;

public class Levitator : MonoBehaviour
{
    private const float MAX_SIN_TIME = Mathf.PI * 2f;

    [SerializeField] private float frequency = 5f;
    [SerializeField] private float magnitude = 0.1f;
    [SerializeField] private float leviationHeight = 1.5f;
    [SerializeField] private float pushSpeed = 5f;
    [SerializeField] private float rotationSpeed = 50f;

    private float time;
    private float localHeight;

    public void Levitate()
    {
        CountTime(frequency);
        localHeight = Mathf.Sin(time) * magnitude;

        PushOffTheGround();
        Rotate();
    }

    private void CountTime(float frequency)
    {
        time += frequency * Time.deltaTime;

        if (time > MAX_SIN_TIME)
            time -= MAX_SIN_TIME;
    }

    private void PushOffTheGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, ComplexLayers.Landscape))
        {
            float currentHeight = transform.position.y - localHeight;
            float targetHeight = hit.point.y + leviationHeight;

            float adjustedHeight = Mathf.Lerp(currentHeight, targetHeight, pushSpeed * Time.deltaTime);
            transform.position = transform.position.SetY(adjustedHeight + localHeight);
        }
    }

    private void Rotate()
    {
        transform.eulerAngles += new Vector3(0f, rotationSpeed, 0f) * Time.deltaTime;
    }
}