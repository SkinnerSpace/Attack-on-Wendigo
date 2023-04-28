public interface IHealthPack : IItem
{
    void Use();
    void SetTarget(IHealthSystem healthSystem);
}
