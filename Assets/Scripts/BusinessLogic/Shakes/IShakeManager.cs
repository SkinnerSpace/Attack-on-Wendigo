public interface IShakeManager
{
    void Handle(IShake shake);
    void AddAndLaunch(IShake shake);
}
