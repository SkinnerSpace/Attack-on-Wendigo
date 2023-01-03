using System;
using UnityEngine;

public class WeaponSwayController : MonoBehaviour
{
    [SerializeField] private bool displacementOn = true;
    private WeaponDisplacer displacer;

    [SerializeField] private bool rotationOn = true;
    private WeaponRotator rotator;

    [SerializeField] private bool tiltOn = true;
    private WeaponTilter tilter;

    [SerializeField] private bool oscillationOn = true;
    private WeaponOscillator oscillator;

    private Quaternion tiltedRotation = Quaternion.identity;
    private Quaternion offsettedRotation = Quaternion.identity;

    public Vector2 input { get; private set; }

    private void Awake()
    {
        displacer = GetComponent<WeaponDisplacer>();
        rotator = GetComponent<WeaponRotator>();
        tilter = GetComponent<WeaponTilter>();
        oscillator = GetComponent<WeaponOscillator>();
    }

    private void Update()
    {
        ReadInput();

        if (displacementOn) DisplacePosition();
        if (rotationOn) OffsetRotation();
        if (tiltOn) TiltRotation();

        transform.localRotation = offsettedRotation * tiltedRotation;
    }

    private void ReadInput()
    {
        Vector2 oscillation = oscillationOn ? oscillator.movement : Vector2.one;

        input = new Vector2(
            InputReader.mouse.x + oscillation.x,
            InputReader.mouse.y + oscillation.y);
    }

    private void DisplacePosition()
    {
        transform.localPosition = displacer.DisplacePosition(transform.localPosition);
    }

    private void OffsetRotation()
    {
        offsettedRotation = rotationOn ? rotator.OffsetRotation(offsettedRotation) : Quaternion.identity;
    }

    private void TiltRotation()
    {
        tiltedRotation = tiltOn ? tilter.TiltRotation(tiltedRotation) : Quaternion.identity;
    }
}
