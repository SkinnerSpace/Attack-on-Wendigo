using UnityEngine;

[ExecuteAlways]
public class BezierPoint : MonoBehaviour
{
    public Vector3 Position => transform.position;

    private BezierTrajectory trajectory;
    private Vector3 prevPos;

    private void OnDrawGizmos() => NotifyOnChange();

    public void SetPosition(Vector3 position) => transform.position = position;

    public void SetTrajectory(BezierTrajectory trajectory) => this.trajectory = trajectory;

    private void NotifyOnChange()
    {
        if (prevPos != transform.position){
            trajectory.UpdatePoints();
            prevPos = transform.position;
        }
    }
}