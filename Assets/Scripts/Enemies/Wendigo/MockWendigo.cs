using UnityEngine;

public class MockWendigo : IWendigo
{
    public WendigoData Data { get; set; }
    public IHitBox[] HitBoxes { get; set; }

    public MockWendigo(WendigoData data) => Data = data;

    public MockWendigo WithHitBoxes(int amount)
    {
        HitBoxes = new HitBox[amount];

        for (int i = 0; i < amount; i++)
            HitBoxes[i] = new HitBox();

        return this;
    }

    public void SetTarget(Transform target)
    {
        
    }
}