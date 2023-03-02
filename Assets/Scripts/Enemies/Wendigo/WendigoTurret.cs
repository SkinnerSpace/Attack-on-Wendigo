using UnityEngine;

public class WendigoTurret : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float maxDistance = 100f;

    [Range(0,180)]
    [SerializeField] private float horizontalAngle;
    private float horizontalThreshold => 1f - (horizontalAngle / 180f);

    private Vector3 point;

    private void OnDrawGizmos()
    {
        //Debug.Log(horizontalThreshold);

        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float horizontalDot = Vector3.Dot(transform.forward.FlatV3(), direction.FlatV3());

            if (horizontalDot >= horizontalThreshold)
            {
                point.x = target.position.x;
                point.y = transform.position.y;
                point.z = target.position.z;
            }

            Vector3 pointDirection = (point - transform.position).normalized;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (pointDirection * 100f));
        }
    }
}
