using System;
using UnityEngine;

public class WeaponSwayController : MonoBehaviour, IMouseMotionObserver
{
    private const float SLOW_DOWN = 0.01f;

    [SerializeField] private bool displacementOn = true;
    private WeaponDisplacer displacer;

    [SerializeField] private bool rotationOn = true;
    private WeaponRotator rotator;

    [SerializeField] private bool tiltOn = true;
    private WeaponTilter tilter;

    [SerializeField] private bool oscillationOn = true;
    private WeaponOscillator oscillator;

    private Weapon weapon;
    private DisplacementCorrector corrector;
    private VerticalTuner verticalTuner;

    private Quaternion tiltedRotation = Quaternion.identity;
    private Quaternion offsettedRotation = Quaternion.identity;

    public Vector2 input { get; private set; }
    private IInputReader inputReader;

    private bool isReady;

    public void Initialize(Weapon weapon, ICharacterData characterData, IInputReader inputReader)
    {
        this.weapon = weapon;
        verticalTuner = new VerticalTuner(characterData);
        displacer = GetComponent<WeaponDisplacer>().Initialize(verticalTuner);
        rotator = GetComponent<WeaponRotator>().Initialize(verticalTuner);
        tilter = GetComponent<WeaponTilter>().Initialize(verticalTuner);
        oscillator = GetComponent<WeaponOscillator>().Initialize(characterData);
        corrector = GetComponent<DisplacementCorrector>();

        weapon.SubscribeOnReady(OnReady);
        this.inputReader = inputReader;
    }

    public void OnReady(bool isReady)
    {
        this.isReady = isReady;

        if (isReady)
        {
            inputReader.Get<MouseMotionInputReader>().Subscribe(this);
        }
        else if (!isReady)
        {
            inputReader.Get<MouseMotionInputReader>().Unsubscribe(this);
            corrector.Fix(weapon.Data);
        }
    }

    private void Update() => Sway();

    private void Sway()
    {
        if (isReady)
        {
            UpdateActiveDisplacers();

            transform.localPosition = GetPositionDisplacement();
            transform.localRotation = GetRotationDisplacement();
        }
    }

    private void UpdateActiveDisplacers()
    {
        if (displacementOn) displacer.ReadInput();
        if (rotationOn) rotator.ReadInput();
        if (tiltOn) tilter.ReadInput();
        if (oscillationOn) oscillator.ReadInput();
    }

    public void ReceiveMotion(Vector2 motion)
    {
        Vector2 oscillation = oscillationOn ? oscillator.movement : Vector2.zero;

        input = new Vector2(
            (motion.x * SLOW_DOWN) + oscillation.x,
            (motion.y * SLOW_DOWN) + oscillation.y);
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
}
