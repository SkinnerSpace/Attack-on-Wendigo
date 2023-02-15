using UnityEngine;

public class DampedSpring : IGroundObserver
{
    private ICharacterData data;
    private IChronos chronos;

    public DampedSpring(ICharacterData data, IChronos chronos)
    {
        this.data = data;
        this.chronos = chronos;
    }

    public void OnGrounded()
    {
        data.CurrentDampedSpringTime = 0f;

        float maxVerticalVelocity = Mathf.Sqrt(data.JumpHeight * data.MaxJumpCount * data.Gravity * 2f);
        data.DampedSpringAmplitude = Mathf.Abs(data.VerticalVelocity / maxVerticalVelocity) * data.DampedSpringPower;
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