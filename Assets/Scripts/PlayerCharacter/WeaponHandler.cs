using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private CombatInputReader inputReader;
    private IWeapon weapon;

    private void Awake() => weapon = NullWeapon.Instance;

    public void SetCombatInputReader(CombatInputReader inputReader) => this.inputReader = inputReader;

    public void SetWeapon(IWeapon weapon)
    {
        this.weapon = weapon;
        inputReader.Subscribe(weapon);
    }

    public void ResetWeapon()
    {
        inputReader.Unsubscribe(weapon);
        weapon = NullWeapon.Instance;
    }
}
