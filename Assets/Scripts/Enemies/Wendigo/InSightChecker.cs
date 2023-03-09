using UnityEngine;
using WendigoCharacter;

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
        bool targetExist = data.Target != null;

        float dotToTarget = targetExist ? GetDotToTarget(data.Target) : 0f;
        data.Head.OnTarget = targetExist ? dotToTarget <= GetMaxDotProduct(data.Head.LookAngleOfView) : false;
        data.Firebreath.OnTarget = targetExist ? dotToTarget <= GetMaxDotProduct(data.Firebreath.AngleOfView) : false;
        data.Fireball.OnTarget = targetExist ? dotToTarget <= GetMaxDotProduct(data.Fireball.AngleOfView) : false;
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
