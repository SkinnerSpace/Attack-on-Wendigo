using UnityEngine;
using System;
using Character;

public class FOVController : BaseController, IMoverObserver
{
    private PlayerCharacter main;
    private ICharacterData data;
    private IChronos chronos;

    private float runMultiplier = 0.2f;
    private float dashMultiplier = 0.8f;
    private float drugsMultiplier = 0.8f;
    
    private (float run, float dash, float drugs) multipliers;
    private float targetRunMultiplier;

    private float totalMultiplier;

    private float fOVMultiplierLerp = 4f;
    private float fOVChangeLerp = 10f;
    private float lerp;

    private float targetFOV;

    public override void Initialize(PlayerCharacter main)
    {
        this.main = main;

        data = main.OldData;
        chronos = main.Chronos;
    }

    public override void Connect()
    {
        main.Mover.Subscribe(this);
        main.GetController<MovementController>().Subscribe(OnRunUpdate);
        main.GetController<DashController>().Subscribe(OnDash);
        PlayerEvents.current.onHealthRestore += OnHealthRestore;
    }

    public override void Disconnect() { }

    public void Update()
    {
        UpdateFOV();
        DecreaseFOVOverTime();
    }

    private void UpdateFOV()
    {
        totalMultiplier = multipliers.run + multipliers.dash + multipliers.drugs;

        targetFOV = data.DefaultFOV + (data.AdditionalFOV * totalMultiplier);
        data.FOV = Mathf.Lerp(data.FOV, targetFOV, fOVChangeLerp * chronos.DeltaTime);
    }

    private void DecreaseFOVOverTime()
    {
        lerp = fOVMultiplierLerp * chronos.DeltaTime;

        multipliers.run = Mathf.Lerp(multipliers.run, targetRunMultiplier, lerp);
        multipliers.dash = Mathf.Lerp(multipliers.dash, 0f, lerp);
        multipliers.drugs = Mathf.Lerp(multipliers.drugs, 0f, lerp);
    }

    public void OnRunUpdate(bool isRunning) => targetRunMultiplier = isRunning ? runMultiplier : 0f;

    public void OnDash() => multipliers.dash = dashMultiplier;

    public void OnHealthRestore() => multipliers.drugs = drugsMultiplier;
}

