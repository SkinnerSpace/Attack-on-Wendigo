using UnityEngine;

public class TrajectoryLine : MonoBehaviour, ITrajectoryRenderer
{
    private LineRenderer line;

    private void Awake() => line = GetComponent<LineRenderer>();

    public void Render(Trajectory trajectory)
    {
        Vector3[] points = trajectory.GetPoints();

        line.positionCount = points.Length;
        line.SetPositions(points);
    }

    public void TurnOn() => line.enabled = true;
    public void TurnOff() => line.enabled = false;
}
