using System;
using UnityEngine;

public class HelicopterSway : MonoBehaviour, IHelicopterTimeObserver
{
    [SerializeField] private float maxSwayAngle = 15f;

    [Header("Magnitude")]
    [SerializeField] private float flyingMagnitude = 1f;
    [SerializeField] private float landingMagnitude = 0.2f;
    [SerializeField] private float magnitudeChangeSpeed = 0.5f;

    [SerializeField] private float waveSpeed = 1f;

    private float time;
    private float movement;
    private float wave;

    private float targetMagnitude;
    private float waveMagnitude = 1f;

    private void Awake()
    {
        targetMagnitude = flyingMagnitude;
        waveMagnitude = flyingMagnitude;
    }

    public void UpdateCompletion(float completion)
    {
        movement = Easing.QuadEaseInOut(completion) * Mathf.PI;
        movement = Mathf.Sin(movement);
    }

    private void Update()
    {
        TiltForward();
        Wave();
        UpdateMagnitude();
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

    public void SetFlyingMagnitude() => targetMagnitude = flyingMagnitude;
    public void SetLandingMagnitude() => targetMagnitude = landingMagnitude;

    private void UpdateMagnitude()
    {
        waveMagnitude = Mathf.Lerp(waveMagnitude, targetMagnitude, magnitudeChangeSpeed * Time.deltaTime);
    }
}
