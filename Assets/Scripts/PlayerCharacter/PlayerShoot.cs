using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Weapon weapon;

    private IKeyBinds keys;

    private void Awake()
    {
        keys = GetComponent<IKeyBinds>();
    }

    private void Update()
    {
        ReadInput();
    }

    
    private void ReadInput()
    {
        if (Input.GetKey(keys.Shoot))
            weapon.PullTheTrigger();

        weapon.Aim(Input.GetKey(keys.Aim));

        if (Input.GetKey(keys.Reload))
            weapon.Reload();
    }
}
