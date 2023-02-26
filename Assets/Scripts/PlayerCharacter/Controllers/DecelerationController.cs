using UnityEngine;

public class DecelerationController : BaseController, IMoverObserver
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
        SetDeceleration();
        ApplyDeceleration();
    }

    public void SetDeceleration() => data.Deceleration = data.IsGrounded ? data.GroundDeceleration : data.AirDeceleration;

    public void ApplyDeceleration()
    {
        data.FlatVelocity = Vector2.Lerp(data.FlatVelocity, Vector2.zero, data.Deceleration * chronos.DeltaTime);
    }
}