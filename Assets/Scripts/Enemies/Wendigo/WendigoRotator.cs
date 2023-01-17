using UnityEngine;

public class WendigoRotator : MonoBehaviour
{
    [SerializeField] private float speed = 30f;

    public void RotateToTarget(Vector3 targetPos)
    {
        Vector3 flatTargetPos = new Vector3(targetPos.x, 0f, targetPos.z);
        Vector3 flatOwnPos = new Vector3(transform.position.x, 0f, transform.position.z);

        Vector3 dirToTarget = (flatTargetPos - flatOwnPos).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(dirToTarget, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
}
