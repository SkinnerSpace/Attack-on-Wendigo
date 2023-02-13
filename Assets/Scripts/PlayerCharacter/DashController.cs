using UnityEngine;

public class DashController
{
    private ICharacterData data;
    private IChronos chronos;
    private FunctionTimer timer;
    private bool disabled;

    public DashController(ICharacterData data, IChronos chronos, FunctionTimer timer)
    {
        this.data = data;
        this.chronos = chronos;
        this.timer = timer;
    }

    public void Dash()
    {
        if (!disabled)
        {
            disabled = true;
            Vector2 direction = data.Forward.FlatV2();
            Vector2 dashVelocity = direction * GetDashPower();
            data.FlatVelocity += dashVelocity;
        }
    }

    public float GetDashPower()
    {
        float dragAdjustment = (data.Deceleration * chronos.DeltaTime) + 1f;
        float distanceAdjustment = data.Deceleration * 0.1f;
        float resistance = Mathf.Log(1f / dragAdjustment) / chronos.DeltaTime.Negative();
        
        float dashPower = (data.DashDistance + distanceAdjustment) * resistance;
        return dashPower;
    }
}


