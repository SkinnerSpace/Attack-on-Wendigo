using System.Collections;
using UnityEngine;

public class WeaponSweeper : MonoBehaviour
{
    [SerializeField] private Rigidbody weapon;

    public void SweepTheWeapon() => StartCoroutine(Sweep());

    private IEnumerator Sweep()
    {
        Debug.Log("Sweep!");
        while (weapon.velocity.magnitude != 0f)
        {
            yield return null;
        }
        Debug.Log("FELL ASLEEP!");
    }
}
