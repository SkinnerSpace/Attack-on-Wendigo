using UnityEngine;
using WendigoCharacter;

public class InSightChecker : MonoBehaviour
{
    [SerializeField] private WendigoData data;

    private void Update()
    {
        bool targetExist = data.Target != null;

        float dotToTarget = targetExist ? GetDotToTarget(data.Target.Position) : 0f;
        data.Head.OnTarget = targetExist ? dotToTarget <= GetMaxDotProduct(data.Head.LookAngleOfView) : false;
        data.Firebreath.OnTarget = targetExist ? dotToTarget <= GetMaxDotProduct(data.Firebreath.AngleOfView) : false;
        data.Fireball.OnTarget = targetExist ? dotToTarget <= GetMaxDotProduct(data.Fireball.AngleOfView) : false;
    }

    private float GetDotToTarget(Vector3 targetPosition)
    {
        Vector2 forwardDir = GetForwardDir();
        Vector2 dirToTarget = GetDirToTarget(targetPosition);
        float dotToTarget = GetDotToTarget(forwardDir, dirToTarget);

        return dotToTarget;
    }

    private Vector2 GetForwardDir() => new Vector2(transform.forward.x, transform.forward.z);

    private Vector2 GetDirToTarget(Vector3 targetPosition)
    {
        Vector2 position = transform.position.FlatV2();
        Vector2 flatTargetPosition = targetPosition.FlatV2();
        Vector2 dirToTarget = (flatTargetPosition - position).normalized;

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
