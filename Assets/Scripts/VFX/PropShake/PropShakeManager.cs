using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropShakeManager : MonoBehaviour
{
    [SerializeField] private ShakeableProp shakeable;
    [SerializeField] private float time = 2f;
    [SerializeField] private float power = 0.5f; 
    [SerializeField] private float frequency = 30f;
    [SerializeField] private float attack = 0.1f;
    [SerializeField] private float release = 0.25f;

    private Shake shake;

    private void Update() => UpdateShakes();

    private void UpdateShakes()
    {
        if (shake != null)
        {
            if (shake.isActive)
            {
                ShakeHandler.Handle(shake);
                shakeable.Displace(ShakeHandler.GetDisplacement(shake));
            }
        }
    }

    public void Launch()
    {
        ShakeTimer timer = new ShakeTimer(time);
        Vector3 axis = new Vector3(1f, 0f, 1f);
        ShakeStrength strength = new ShakeStrength(power, 0f);
        ShakeCurve curve = new ShakeCurve(frequency, attack, release);
        float attenuation = 1f;

        shake = new Shake(timer, axis, strength, curve, attenuation);
        ShakeHandler.Launch(shake);
    }
}
