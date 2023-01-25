using System.Collections.Generic;
using UnityEngine;

public class TrajectoryTarget : MonoBehaviour, ITrajectoryRenderer
{
    private MeshRenderer mesh;
    private void Awake() => mesh = GetComponent<MeshRenderer>();

    public void Render(Trajectory trajectory)
    {
        EndPoint endPoint = trajectory.GetEndPoint();
        transform.SetPositionAndRotation(endPoint.position, endPoint.rotation);
    }

    public void TurnOn() => mesh.enabled = true;
    public void TurnOff() => mesh.enabled = false;
}
