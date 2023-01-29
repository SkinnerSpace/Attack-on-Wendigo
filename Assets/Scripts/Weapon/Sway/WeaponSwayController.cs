using System;
using UnityEngine;

public class WeaponSwayController : MonoBehaviour, ISpeedObserver
{
    [Header("RequiredComponents")]
    [SerializeField] private Transform weaponImp;
    private IWeapon weapon;

    [Header("Settings")]
    [SerializeField] private bool displacementOn = true;
    private WeaponDisplacer displacer;

    [SerializeField] private bool rotationOn = true;
    private WeaponRotator rotator;

    [SerializeField] private bool tiltOn = true;
    private WeaponTilter tilter;

    [SerializeField] private bool oscillationOn = true;
    private WeaponOscillator oscillator;

    private DisplacementCorrector corrector;

    private Quaternion tiltedRotation = Quaternion.identity;
    private Quaternion offsettedRotation = Quaternion.identity;

    public Vector2 input { get; private set; }

    private void Awake()
    {
        weapon = weaponImp.GetComponent<IWeapon>();

        displacer = GetComponent<WeaponDisplacer>();
        rotator = GetComponent<WeaponRotator>();
        tilter = GetComponent<WeaponTilter>();
        oscillator = GetComponent<WeaponOscillator>();
        corrector = GetComponent<DisplacementCorrector>();
    }

    private void Update() => Sway();

    private void Sway()
    {
        if (weapon.isReady)
        {
            UpdateActiveDisplacers();
            ReadInput();

            transform.localPosition = GetPositionDisplacement();
            transform.localRotation = GetRotationDisplacement();
        }
    }

    public void ResetSway() => corrector.Fix(weapon);

    private void ReadInput()
    {
        Vector2 oscillation = oscillationOn ? oscillator.movement : Vector2.one;

        input = new Vector2(
            InputReader.mouse.x + oscillation.x,
            InputReader.mouse.y + oscillation.y);
    }

    private void UpdateActiveDisplacers()
    {
        if (displacementOn) displacer.ReadInput();
        if (rotationOn) rotator.ReadInput();
        if (tiltOn) tilter.ReadInput();
        if (oscillationOn) oscillator.ReadInput();
    }

    private Vector3 GetPositionDisplacement() => displacer.DisplacePosition(transform.localPosition);

    private Quaternion GetRotationDisplacement()
    {
        offsettedRotation = OffsetRotation(offsettedRotation);
        tiltedRotation = TiltRotation(tiltedRotation);

        return offsettedRotation * tiltedRotation;
    }

    private Quaternion OffsetRotation(Quaternion rotation) => rotationOn ? rotator.OffsetRotation(rotation) : Quaternion.identity;
    private Quaternion TiltRotation(Quaternion rotation) => tiltOn ? tilter.TiltRotation(rotation) : Quaternion.identity;

    public void ConnectSpeedometer(Speedometer speedometer) => oscillator.ConnectSpeedometer(speedometer);
}
