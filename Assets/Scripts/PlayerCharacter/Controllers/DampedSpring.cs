using UnityEngine;

public class DampedSpring : BaseController, IGroundObserver, IMoverObserver
{
    private const float MAX_MULTIPLIER = 2f;
    private const float MAX_AMPLITUDE = 1.8f;

    private MainController main;
    private ICharacterData data;
    private IChronos chronos;

    public override void Initialize(MainController main)
    {
        this.main = main;
        data = main.Data;
        chronos = main.Chronos;
    }

    public override void Connect()
    {
        main.Mover.Subscribe(this);
        main.GetController<GroundDetector>().Subscribe(this);
    }

    public void Land()
    {
        data.CurrentDampedSpringTime = 0f;

        float maxVerticalVelocity = Mathf.Sqrt(data.JumpHeight * data.MaxJumpCount * data.Gravity * MAX_MULTIPLIER);
        data.DampedSpringAmplitude = Mathf.Abs(data.PreviousVerticalVelocity / maxVerticalVelocity) * data.DampedSpringPower;
        data.DampedSpringAmplitude = Mathf.Clamp(data.DampedSpringAmplitude, 0f, MAX_AMPLITUDE);
    }

    public void Update()
    {
        if (data.CurrentDampedSpringTime < data.DampedSpringTime)
        {
            data.CurrentDampedSpringTime += chronos.DeltaTime;

            float interpolation = (data.CurrentDampedSpringTime / data.DampedSpringTime).Clamp01();
            interpolation = Easing.QuadEaseOut(interpolation);

            float motion = -1 * Mathf.Sin(interpolation * Mathf.PI) * data.DampedSpringAmplitude;
            data.CameraLocalPos = new Vector3(data.CameraLocalPos.x, motion, data.CameraLocalPos.z);
        }
    }
}