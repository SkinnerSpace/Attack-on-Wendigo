using UnityEngine;

public class LogoAnimationFrame : MonoBehaviour, ISwitchable
{
    public LogoAnimationStages stage;
    public void SwitchOn() => gameObject.SetActive(true);
    public void SwitchOff() => gameObject.SetActive(false);
}

