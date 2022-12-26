public interface ILeg
{
    ITransformProxy Transform { get; }
    float Side { get; }
    bool IsEquipped { get; }
    void SetNextStep();
    void Update();
    void StepIsOver();
}