using System;
using UnityEngine;

public class HelicopterSway : MonoBehaviour, IHelicopterTimeObserver
{
    [SerializeField] private float maxSwayAngle = 15f;
    [SerializeField] private float waveMagnitude = 8f;
    [SerializeField] private float waveSpeed = 1f;

    private float time;
    private float movement;
    private float wave;

    public void UpdateCompletion(float completion)
    {
        movement = ExtraMath.QuadInOut(completion) * Mathf.PI;
        movement = Mathf.Sin(movement);
    }

    private void Update()
    {
        TiltForward();
        Wave();
    }

    private void TiltForward()
    {
        Vector3 angles = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(maxSwayAngle * movement, angles.y, angles.z);
    }

    private void Wave()
    {
        time += waveSpeed * OldChronos.DeltaTime;
        wave = Mathf.Sin(time / (Mathf.PI * 2f));
        wave *= waveMagnitude;
        wave *= (1f - movement);

        Vector3 wavePosition = new Vector3(0f, wave, 0f);
        transform.localPosition = wavePosition;
    }
}
