using System.Collections.Generic;

public class ShakeManager : IShakeManager
{
    private List<Shake> shakes = new List<Shake>();
    private ShakeDisplacement displacementTotal = new ShakeDisplacement();

    private IShakeable shakeable;

    public ShakeManager(IShakeable shakeable) => this.shakeable = shakeable;

    public void AddAndLaunch(Shake shake)
    {
        shakes.Add(shake);
        shake.Launch();
    }

    public void Update()
    {
        displacementTotal.Clear();

        for (int i = shakes.Count - 1; i >= 0; i--)
            HandleIfActive(i);

        shakeable.Displace(displacementTotal);
    }

    private void HandleIfActive(int index)
    {
        if (shakes[index].isActive){
            Handle(shakes[index]);
        }
        else if (!shakes[index].isActive){
            shakes.RemoveAt(index);
        }
    }

    public void Handle(Shake shake)
    {
        shake.Proceed();
        ShakeDisplacement displacement = shake.GetDisplacement();
        displacementTotal.Accumulate(displacement);
    }

    public int GetShakesCount() => shakes.Count;
}
