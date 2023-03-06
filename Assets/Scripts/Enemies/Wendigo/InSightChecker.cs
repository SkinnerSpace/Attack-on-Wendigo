using UnityEngine;

public class InSightChecker : MonoBehaviour
{
    [SerializeField] private Wendigo wendigo;

    private WendigoData data;

    private void Start()
    {
        data = wendigo.Data;
    }

    private void Update()
    {
        float dotToTarget = (data.Target != null) ? GetDotToTarget(data.Target) : 0f;
        data.TargetFitsLookAngle = (data.Target != null) ? dotToTarget <= GetMaxDotProduct(data.LookAngleOfView) : false;
        data.TargetFitsFirebreathAngle = (data.Target != null) ? dotToTarget <= GetMaxDotProduct(data.FirebreathAngleOfView) : false;
        data.TargetFitsFireballAngle = (data.Target != null) ? dotToTarget <= GetMaxDotProduct(data.AttackAngleOfView) : false;
    }

    private float GetDotToTarget(Transform target)
    {
        Vector2 forwardDir = GetForwardDir();
        Vector2 dirToTarget = GetDirToTarget(target);
        float dotToTarget = GetDotToTarget(forwardDir, dirToTarget);

        return dotToTarget;
    }

    private Vector2 GetForwardDir() => new Vector2(transform.forward.x, transform.forward.z);

    private Vector2 GetDirToTarget(Transform target)
    {
        Vector2 position = transform.position.Flatten();
        Vector2 targetPosition = target.position.Flatten();
        Vector2 dirToTarget = (targetPosition - position).normalized;

        return dirToTarget;
    }

    private float GetDotToTarget(Vector2 forwardDir, Vector2 dirToTarget)
    {
        float dotToTarget = Vector2.Dot(forwardDir, dirToTarget);
        dotToTarget = Mathf.Abs(dotToTarget - 1f);
        dotToTarget = Mathf.Clamp(dotToTarget, 0f, 1f);

        return dotToTarget;
    }

    private float GetMaxDotProduct(float angle) => (angle * Mathf.Deg2Rad) / Mathf.PI;
}
