using System;

public class Walker
{
    private const float VELOCITY_THRESHOLD = 0.1f;

    private ICharacterData data;
    private IChronos chronos;

    public float stepSpacing { get; set; } = 5f;
    public float stepProgress { get; private set; }
    public bool firstStepIsMade { get; private set; }

    public Walker(ICharacterData data, IChronos chronos)
    {
        this.data = data;
        this.chronos = chronos;
    }

    public void Walk(Action playSFX)
    {
        float moveDistance = data.FlatVelocity.magnitude * chronos.DeltaTime;

        if (data.IsGrounded && data.FlatVelocity.magnitude >= VELOCITY_THRESHOLD)
        {
            MakeAStep(moveDistance, playSFX);
        }
        else
        {
            Interrupt();
        }
    }

    private void MakeAStep(float distance, Action playSFX)
    {
        stepProgress = firstStepIsMade ? (stepProgress + distance) : stepSpacing;
        firstStepIsMade = true;

        if (stepProgress >= stepSpacing)
        {
            stepProgress = 0f;
            playSFX();
        }
    }

    private void Interrupt()
    {
        firstStepIsMade = false;
        stepProgress = 0f;
    }
}
