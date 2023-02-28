public interface IAmmoObserver
{
    void SetActive(bool active);
    void OnUpdate(int ammo);
}
