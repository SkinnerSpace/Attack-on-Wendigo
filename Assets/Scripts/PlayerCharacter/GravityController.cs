public class GravityController
{
    public ICharacterData data { get; private set; }

    public GravityController(ICharacterData data) => this.data = data;

    public void ApplyGravity()
    {
        data.VerticalVelocity -= data.Gravity * Chronos.DeltaTime;
    }
}