using UnityEngine;

public class GravityController : BaseController, IGroundObserver, IMoverObserver
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

    public override void Connect()
    {
        main.Mover.Subscribe(this);
        main.GetController<GroundDetector>().Subscribe(this);
    }

    public void Update()
    {
        if (!data.IsGrounded)
            data.VerticalVelocity -= data.Gravity * chronos.DeltaTime;
    }

    public void OnGrounded() => data.VerticalVelocity = 0f;
}
