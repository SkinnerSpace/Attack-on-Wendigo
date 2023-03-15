public class CharacterBurnController : BaseController
{
    private CharacterHealthSystem healthSystem;
    private BurnHandler burnHandler;

    private float scorchTime = 1f;
    private int scorchDamage = 1;

    public override void Initialize(PlayerCharacter main)
    {
        healthSystem = main.GetController<CharacterHealthSystem>();
        burnHandler = new BurnHandler(main.FireHitBox, main.Timer, scorchTime);
    }

    public override void Connect() => burnHandler.SubscribeOnScorch(ReceiveDamage);
    public override void Disconnect() { }

    public void Subscribe(IBurnObserver observer) => burnHandler.Subscribe(observer);

    private void ReceiveDamage()
    {
        DamagePackage damagePackage = new DamagePackage(scorchDamage);
        healthSystem.ReceiveDamage(damagePackage);
    }
}
