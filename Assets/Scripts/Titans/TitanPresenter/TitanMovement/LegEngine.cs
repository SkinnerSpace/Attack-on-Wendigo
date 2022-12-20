using UnityEngine;

public class LegEngine
{
    private float stepHeight = 3f;

    private bool active = false;
    private float lerp = 0f;
    
    private Leg leg;

    public LegEngine(Leg leg, LegSetupPack setupPack)
    {
        this.leg = leg;
        stepHeight = setupPack.stepHeight;
    }

    public void SetActive(bool active)
    {
        lerp = 0f;
        this.active = active;
    }

    public Vector3 UpdatePosition(Vector3 oldPos, Vector3 newPos, float speed)
    {
        if (active)
        {
            IncrementLerp(speed);
            return CalculatePosition(oldPos, newPos);
        }
        return oldPos;
    }

    private void IncrementLerp(float speed)
    {
        lerp += speed * Time.deltaTime;
        if (lerp >= 1f)
        {
            lerp = 1f;
            active = false;
            leg.StepIsOver();
        }
    }

    private Vector3 CalculatePosition(Vector3 oldPos, Vector3 newPos)
    {
        Vector3 footPos = Vector3.Lerp(oldPos, newPos, lerp);
        footPos.y = footPos.y + (Mathf.Sin(lerp * Mathf.PI) * stepHeight);
        return footPos;
    }
}