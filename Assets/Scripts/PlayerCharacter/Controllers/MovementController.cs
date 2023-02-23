﻿using UnityEngine;

public class MovementController : BaseController, IMovementController
{
    private ICharacterData data;
    private IChronos chronos;
    private IInputReader input;

    public override void Initialize(MainController main) => Initialize(main.Data, main.Chronos, main.InputReader);

    public void Initialize(ICharacterData data, IChronos chronos, IInputReader input)
    {
        this.data = data;
        this.chronos = chronos;
        this.input = input;
    }

    public override void Connect() => input.Get<MovementInputReader>().Subscribe(this);
    public void Disconnect() => input.Get<MovementInputReader>().Unsubscribe(this);

    public void Move(Vector3 inDirection)
    {
        Vector2 direction = ((inDirection.x * data.Right) + (inDirection.z * data.Forward)).FlatV2();
        Vector2 acceleration = direction * data.Speed * chronos.DeltaTime;
        data.FlatVelocity += acceleration;
    }
}

