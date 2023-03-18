using Character;
using UnityEngine;

public class DampedSpring : BaseController, IGroundObserver, IMoverObserver
{
    private const float MAX_MULTIPLIER = 2f;
    private const float MAX_AMPLITUDE = 1.8f;

    private PlayerCharacter main;
    private ICharacterData data;
    private IChronos chronos;

    public override void Initialize(PlayerCharacter main)
    {
        this.main = main;
        Initialize(main.OldData, main.Chronos);
    }

    public void Initialize(ICharacterData data, IChronos chronos)
    {
        this.data = data;
        this.chronos = chronos;
    }

    public override void Connect()
    {
        main.Mover.Subscribe(this);
        main.GetController<GroundDetector>().Subscribe(this);
    }

    public override void Disconnect() { }

    public void Land()
    {
        data.DampedSpring.CurrentTime = 0f;

        float maxVerticalVelocity = Mathf.Sqrt(data.JumpHeight * data.MaxJumpCount * data.Gravity * MAX_MULTIPLIER);
        data.DampedSpring.Amplitude = Mathf.Abs(data.PreviousVerticalVelocity / maxVerticalVelocity) * data.DampedSpring.Power;
        data.DampedSpring.Amplitude = Mathf.Clamp(data.DampedSpring.Amplitude, 0f, MAX_AMPLITUDE);
    }

    public void Update()
    {
        if (data.DampedSpring.CurrentTime < data.DampedSpring.Time)
        {
            data.DampedSpring.CurrentTime += chronos.DeltaTime;

            float interpolation = (data.DampedSpring.CurrentTime / data.DampedSpring.Time).Clamp01();
            interpolation = Easing.QuadEaseOut(interpolation);

            float motion = -1 * Mathf.Sin(interpolation * Mathf.PI) * data.DampedSpring.Amplitude;
            data.CameraDampedPos = new Vector3(data.CameraDampedPos.x, motion, data.CameraDampedPos.z);
        }
    }
}