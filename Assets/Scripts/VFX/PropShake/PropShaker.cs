using UnityEngine;

public class PropShaker : ICollapseObserver
{
    private float power = 0.5f; 
    private float attack = 0.1f;
    private float release = 0.1f;

    private IShake shake;

    public PropShaker(float frequency){
        shake = ShakeBuilder.Create().WithAxis(1f, 0f, 1f).WithStrength(power, 0f).WithCurve(frequency, attack, release).Build();
    }

    public void ReceiveCollapseUpdate(float progress) => shake.Update(progress); 
    public Vector3 GetPosDisplacement() => shake.GetDisplacement().Position;
}

