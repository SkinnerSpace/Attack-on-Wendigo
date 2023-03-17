public interface IShakeConfig
{
    string Name { get; }
    IShakeConfig SetPower(float power);
    void Launch(IShakeManager shakeManager);
}