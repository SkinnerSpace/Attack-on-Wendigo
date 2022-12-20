public interface ILeg
{
    ITransformProxy transform { get; }
    void MakeAStep();
    void SetSynchronizer(ILegsSync synchronizer);
}