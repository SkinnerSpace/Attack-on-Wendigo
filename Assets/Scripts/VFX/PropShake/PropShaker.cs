using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropShaker : MonoBehaviour
{
    [SerializeField] private float power = 0.5f; 
    [SerializeField] private float attack = 0.1f;
    [SerializeField] private float release = 0.25f;

    private Shake shake;

    public void PrepareShake(float time, float frequency)
    {
        ShakeTimer timer = new ShakeTimer(time);
        Vector3 axis = new Vector3(1f, 0f, 1f);
        ShakeStrength strength = new ShakeStrength(power, 0f);
        ShakeCurve curve = new ShakeCurve(frequency, attack, release);
        float attenuation = 1f;

        shake = new Shake(timer, axis, strength, curve, attenuation);
    }

    public void Launch() => ShakeHandler.Launch(shake);

    public void UpdateShake() => ShakeHandler.Handle(shake);
    public Vector3 GetPosDisplacement() => ShakeHandler.GetDisplacement(shake).position;
    public bool IsDone() => !shake.isActive;
}

