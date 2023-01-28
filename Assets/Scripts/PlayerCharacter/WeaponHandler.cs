using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private IWeapon weapon;

    private IKeyBinds keys;

    private void Awake()
    {
        weapon = NullWeapon.Instance;
        keys = InputReader.Instance.keys;
    }

    private void Update() => ReadInput();
    
    private void ReadInput()
    {
        if (weapon.isReady)
        {
            weapon.PullTheTrigger(Input.GetKey(keys.Shoot));
            weapon.Aim(InputReader.aim);

            if (Input.GetKey(keys.Reload)) weapon.Reload();
        }
    }

    public void SetWeapon(IWeapon weapon) => this.weapon = weapon;
    public void ResetWeapon() => weapon = NullWeapon.Instance;
}
