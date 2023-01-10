using UnityEngine;

public class WendigoRotator : MonoBehaviour
{
    [SerializeField] private float speed = 30f;

    public void RotateToTarget(Transform target)
    {
        Vector3 flatTargetPos = new Vector3(target.position.x, 0f, target.position.z);
        Vector3 flatOwnPos = new Vector3(transform.position.x, 0f, transform.position.z);

        Vector3 vecToTarget = flatTargetPos - flatOwnPos;
        Vector3 dirToTarget = vecToTarget.normalized;
        Quaternion targetRotation = Quaternion.LookRotation(dirToTarget, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
}
