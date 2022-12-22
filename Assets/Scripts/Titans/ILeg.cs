public interface ILeg
{
    ITransformProxy Transform { get; }
    float Side { get; }
    void SetNextStep();
    void SetSynchronizer(ILegsSync synchronizer);
    void Update();
    void StepIsOver();
}