using UnityEngine;

public class FOVController
{
    private ICharacterData data;
    private IChronos chronos;

    public FOVController(ICharacterData data, IChronos chronos)
    {
        this.data = data;
        this.chronos = chronos;
    }

    public void Update()
    {
        float maxVelocity = (data.Speed / data.GroundDeceleration) * 4f;
        float maxPower = data.FlatVelocity.magnitude / maxVelocity;
        maxPower = Mathf.Clamp(maxPower, 0f, 1f);
        maxPower = Easing.QuadEaseInOut(maxPower);
        data.FOVPower = Mathf.Lerp(data.FOVPower, maxPower, data.FOVChangeSpeed * chronos.DeltaTime);

        data.FOV = Mathf.Lerp(data.MinFOV, data.MaxFOV, data.FOVPower);
    }
}

