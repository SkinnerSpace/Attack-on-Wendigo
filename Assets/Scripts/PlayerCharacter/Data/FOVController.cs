using UnityEngine;
using System;

public class FOVController : BaseController, IMoverObserver
{
    private MainController main;
    private ICharacterData data;
    private IChronos chronos;

    private float runMultiplier = 0.2f;
    private float dashMultiplier = 0.8f;
    
    private (float run, float dash) multipliers;
    private float targetRunMultiplier;

    private float totalMultiplier;

    private float fOVMultiplierLerp = 4f;
    private float fOVChangeLerp = 10f;

    private float targetFOV;

    public override void Initialize(MainController main)
    {
        this.main = main;

        data = main.Data;
        chronos = main.Chronos;
    }

    public override void Connect()
    {
        main.Mover.Subscribe(this);
        main.GetController<MovementController>().Subscribe(OnRunUpdate);
        main.GetController<DashController>().Subscribe(OnDash);
    }

    public override void Disconnect() { }

    public void Update()
    {
        totalMultiplier = multipliers.run + multipliers.dash;
        multipliers.run = Mathf.Lerp(multipliers.run, targetRunMultiplier, fOVMultiplierLerp * Time.deltaTime);
        multipliers.dash = Mathf.Lerp(multipliers.dash, 0f, fOVMultiplierLerp * chronos.DeltaTime);

        targetFOV = data.DefaultFOV + (data.AdditionalFOV * totalMultiplier);
        data.FOV = Mathf.Lerp(data.FOV, targetFOV, fOVChangeLerp * chronos.DeltaTime);
    }

    public void OnRunUpdate(bool isRunning) => targetRunMultiplier = isRunning ? runMultiplier : 0f;

    public void OnDash() => multipliers.dash = dashMultiplier;
}

