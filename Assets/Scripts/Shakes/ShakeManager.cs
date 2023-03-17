using System.Collections.Generic;

public class ShakeManager : IShakeManager
{
    private List<IShake> shakes = new List<IShake>();
    private ShakeDisplacement displacementTotal = new ShakeDisplacement();

    private IShakeable shakeable;

    public ShakeManager(IShakeable shakeable) => this.shakeable = shakeable;

    public void AddAndLaunch(IShake shake)
    {
        shakes.Add(shake);
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
        if (shakes[index].IsActive){
            Handle(shakes[index]);
        }
        else if (!shakes[index].IsActive){
            shakes.RemoveAt(index);
        }
    }

    public void Handle(IShake shake)
    {
        shake.Update();
        IShakeDisplacement displacement = shake.GetDisplacement();
        displacementTotal.Accumulate(displacement);
    }

    public int GetShakesCount() => shakes.Count;
}
