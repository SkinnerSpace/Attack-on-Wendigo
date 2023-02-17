using UnityEngine;
using System;

public class FOVController : BaseController, IMoverObserver
{
    private MainController main;
    private ICharacterData data;
    private IChronos chronos;

    public override void Initialize(MainController main)
    {
        this.main = main;

        data = main.Data;
        chronos = main.Chronos;
    }

    public override void Connect() => main.Mover.Subscribe(this);

    public void Update()
    {
        float maxVelocity = (data.Speed / data.GroundDeceleration) * 4f;
        float maxPower = data.FlatVelocity.magnitude / maxVelocity;
        maxPower = Mathf.Clamp(maxPower, 0f, 1f);
        maxPower = Easing.QuadEaseInOut(maxPower);
        data.FOVPower = Mathf.Lerp(data.FOVPower, maxPower, data.FOVChangeSpeed * chronos.DeltaTime);

        data.FOV = Mathf.Lerp(data.MinFOV, data.MaxFOV, data.FOVPower);
    }
}

