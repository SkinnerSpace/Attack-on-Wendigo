public interface IShake
{
    bool IsActive { get; }

    void Update();
    void Update(float progress);
    IShakeDisplacement GetDisplacement();
}