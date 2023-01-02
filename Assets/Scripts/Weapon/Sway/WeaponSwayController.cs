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

    private Quaternion tiltedRotation = Quaternion.identity;
    private Quaternion offsettedRotation = Quaternion.identity;

    private Vector2 input;
    public Action<Vector2> SendInput;

    private void Awake()
    {
        displacer = GetComponent<WeaponDisplacer>();
        if (displacer != null) SendInput += displacer.ReadInput;

        rotator = GetComponent<WeaponRotator>();
        if (rotator != null) SendInput += rotator.ReadInput;

        tilter = GetComponent<WeaponTilter>();
        if (tilter != null) SendInput += tilter.ReadInput;
    }

    private void Update()
    {
        ReadInput();
        SendInput?.Invoke(input);

        if (displacementOn) DisplacePosition();
        if (rotationOn) OffsetRotation();
        if (tiltOn) TiltRotation();

        transform.localRotation = offsettedRotation * tiltedRotation;
    }

    private void ReadInput()
    {
        input = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y"));
    }

    private void DisplacePosition()
    {
        if (displacer != null)
            transform.localPosition = displacer.DisplacePosition(transform.localPosition);
    }

    private void OffsetRotation()
    {
        if (rotator != null)
            offsettedRotation = rotationOn ? rotator.OffsetRotation(offsettedRotation) : Quaternion.identity;
    }

    private void TiltRotation()
    {
        if (tilter != null)
            tiltedRotation = tiltOn ? tilter.TiltRotation(tiltedRotation) : Quaternion.identity;
    }
}
