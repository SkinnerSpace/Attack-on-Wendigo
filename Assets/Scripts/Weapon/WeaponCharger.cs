using UnityEngine;

public class WeaponCharger : MonoBehaviour, IHoldable, IReleaseable
{
    public void Hold()
    {
        Debug.Log("Charge");
    }

    public void Release()
    {
        Debug.Log("BANG!");
    }
}
