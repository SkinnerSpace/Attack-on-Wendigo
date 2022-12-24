public interface ILeg
{
    ITransformProxy Transform { get; }
    float Side { get; }
    void SetNextStep();
    void Update();
    void StepIsOver();
}