using UnityEngine;

public class WaveMover : MonoBehaviour
{
    [Header("Horizontal")]
    [SerializeField] private bool horizontalWave = true;
    [SerializeField] private float horizontalFrequency = 1f;
    [SerializeField] private float horizontalMagnitude = 1f;

    [Header("Vertical")]
    [SerializeField] private bool verticalWave = true;
    [SerializeField] private float verticalFrequency = 1f;
    [SerializeField] private float verticalMagnitude = 1f;

    private SinCounter horizontalOscillator = new SinCounter();
    private SinCounter verticalOscillator = new SinCounter();

    public Vector3 velocity { get; private set; }

    private void Update()
    {
        if (horizontalWave) WaveHorizontally();
        if (verticalWave) WaveVertically();
    }

    private void WaveHorizontally()
    {
        horizontalOscillator.CountTime(horizontalFrequency);
        float wave = (Mathf.Sin(horizontalOscillator.time) * horizontalMagnitude);
        Vector3 horizontalWave = transform.right * wave;

        velocity = new Vector3(horizontalWave.x, velocity.y, horizontalWave.z);
    }

    private void WaveVertically()
    {
        verticalOscillator.CountTime(verticalFrequency);
        float wave = (Mathf.Sin(verticalOscillator.time) * verticalMagnitude);

        velocity = new Vector3(velocity.x, wave, velocity.z);
    }
}
