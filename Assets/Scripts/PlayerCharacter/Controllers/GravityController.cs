using Character;
using UnityEngine;

public class GravityController : BaseController, IGroundObserver, IMoverObserver
{
    private PlayerCharacter main;
    private ICharacterData data;
    private IChronos chronos;

    public override void Initialize(PlayerCharacter main)
    {
        this.main = main;
        data = main.OldData;
        chronos = main.Chronos;
    }

    public override void Connect()
    {
        main.Mover.Subscribe(this);
        main.GetController<GroundDetector>().Subscribe(this);
    }

    public override void Disconnect()
    {
        //main.Mover.Unsubscribe(this);
        //main.GetController<GroundDetector>().Unsubscribe(this);
    }

    public void Update()
    {
        if (!data.IsGrounded)
            data.VerticalVelocity -= data.Gravity * chronos.DeltaTime;
    }

    public void Land() => data.VerticalVelocity = 0f;
}
