using UnityEngine;

public abstract class MenuElement : MonoBehaviour, ISwitchable
{
    public virtual void SwitchOn() => gameObject.SetActive(true);

    public virtual void SwitchOff() => gameObject.SetActive(false);
}
