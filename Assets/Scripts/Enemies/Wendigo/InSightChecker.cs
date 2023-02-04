using UnityEngine;

public class InSightChecker : MonoBehaviour
{
    [SerializeField] private float angleOfView;
    private float maxDotProduct;

    private void Awake() => CalculateAngleOfView();

    public void CalculateAngleOfView() => maxDotProduct = (angleOfView * Mathf.Deg2Rad) / Mathf.PI;

    public bool TargetIsVisibleFromPointOfView(Transform target)
    {
        Vector2 forwardDir = GetForwardDir();
        Vector2 dirToTarget = GetDirToTarget(target);
        float dotToTarget = GetDotToTarget(forwardDir, dirToTarget);

        return dotToTarget <= maxDotProduct;
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
}

public static class VectorExtention
{
    public static Vector2 Flatten(this Vector3 v) => new Vector2(v.x, v.z);
}