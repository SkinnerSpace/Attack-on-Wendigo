using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    [SerializeField] private TrajectoryLine line;
    [SerializeField] private TrajectoryTarget target;

    private bool isRendering;

    public void Render(Trajectory trajectory)
    {
        EnableRendering();
        line.Render(trajectory);
        target.Render(trajectory);
    }

    private void EnableRendering()
    {
        if (!isRendering)
        {
            isRendering = true;
            line.TurnOn();
            target.TurnOn();
        }
    }

    public void DisableRendering()
    {
        isRendering = false;
        line.TurnOff();
        target.TurnOff();
    }
}
