using UnityEngine;

public class TangentLineGenerator : MonoBehaviour
{
    [Range(0f,360f)]
    [SerializeField] private float angle;

    [Range(-50f, 50f)]
    [SerializeField] private float distance;

    [Range(0f, 1f)]
    [SerializeField] private float offset;

    [Range(-50f, 50f)]
    [SerializeField] private float perpDistance;

    private Vector3 Position => transform.position;

    public void DrawLine()
    {

    }

    private void OnDrawGizmos()
    {
        float rads = angle * Mathf.Deg2Rad;
        Vector3 direction = new Vector3(Mathf.Cos(rads), 0f, Mathf.Sin(rads));
        Vector3 perpendicular = new Vector3(-direction.z, 0f, direction.x);

        Vector3 endPos = Position + (direction * distance);
        Vector3 offsetPos = Position + (direction * (offset * distance));
        Vector3 offsetEndPos = offsetPos + (perpendicular * perpDistance);


        DrawLine(Position, endPos, Color.white);
        DrawLine(offsetPos, offsetEndPos, Color.yellow);
    }

    private void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(start, end);
        Gizmos.color = Color.white;
    }
}

