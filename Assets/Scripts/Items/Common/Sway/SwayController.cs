using System;
using UnityEngine;

public class SwayController : MonoBehaviour, IMouseMotionObserver
{
    private const float SLOW_DOWN = 0.01f;

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
    private VerticalTuner verticalTuner;

    private Quaternion tiltedRotation = Quaternion.identity;
    private Quaternion offsettedRotation = Quaternion.identity;

    public Vector2 input { get; private set; }
    private IInputReader inputReader;
    private IHandyItem handyItem;
    public IAimController aimController { get; private set; }

    public bool isReady { get; private set; }

    public void InitializeOnAwake(IHandyItem handyItem, IAimController aimController)
    {
        this.handyItem = handyItem;
        this.aimController = aimController;
    }

    public void InitializeOnAwake(IHandyItem handyItem)
    {
        this.handyItem = handyItem;
    }

    public void InitializeOnTake(ICharacterData characterData, IInputReader inputReader)
    {
        verticalTuner = new VerticalTuner(characterData);
        displacer = GetComponent<WeaponDisplacer>().InitializeOnTake(verticalTuner);
        rotator = GetComponent<WeaponRotator>().InitializeOnTake(verticalTuner);
        tilter = GetComponent<WeaponTilter>();
        oscillator = GetComponent<WeaponOscillator>().InitializeOnTake(characterData);
        corrector = GetComponent<DisplacementCorrector>();

        handyItem.SubscribeOnReady(OnReady);
        handyItem.SubscribeOnNotReady(OnNotReady);

        this.inputReader = inputReader;
    }

    private void OnReady()
    {
        isReady = true;
        inputReader.Get<MouseMotionInputReader>().Subscribe(this);
    }

    private void OnNotReady()
    {
        isReady = false;
        inputReader.Get<MouseMotionInputReader>().Unsubscribe(this);
        corrector.Fix(this);
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
