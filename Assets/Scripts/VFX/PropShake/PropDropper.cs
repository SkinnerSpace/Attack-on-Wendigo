using UnityEngine;

public class PropDropper
{
    private const float DEPTH_OFFSET = 1f;
    private const float PUSH_MULTIPLIER = 15f;

    public Vector3 posDisplacement { get; private set; }
    public Quaternion rotDisplacement { get; private set; }

    private Vector3 originalPos;
    private Quaternion originalRot;

    private Vector3 fallPosOffset;
    private Quaternion fallRotation;

    private float time;
    private float currentTime;
    private float lerp;

    public void PrepareToFall(CollapseEstimator estimator, CollapseAcceptor acceptor)
    {
        time = estimator.time;
        originalPos = acceptor.originalPos;
        originalRot = acceptor.originalRot;

        float depth = -(estimator.height + DEPTH_OFFSET);
        fallPosOffset = new Vector3(0f, depth, 0f);
    }

    public void Launch(Vector3 pushDir)
    {
        fallPosOffset += GetDisplacementFromDirection(pushDir);
        fallRotation = CalculateFallRotation(pushDir, fallPosOffset);
    }

    private Vector3 GetDisplacementFromDirection(Vector3 direction) => new Vector3(direction.x, 0f, direction.z).normalized * PUSH_MULTIPLIER;

    private Quaternion CalculateFallRotation(Vector3 direction, Vector3 offset)
    {
        Vector3 fallPos = originalPos + offset;
        Vector3 fallDir = (fallPos - originalPos).normalized;

        float adjustmentAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion adjustmentRotation = Quaternion.Euler(0f, -adjustmentAngle, 0f);

        return Quaternion.LookRotation(fallDir, Vector3.up) * originalRot * adjustmentRotation;
    }

    public void UpdateFall()
    {
        currentTime += Time.deltaTime;
        lerp = Mathf.Pow((currentTime / time), 2f);

        posDisplacement = Vector3.Lerp(Vector3.zero, fallPosOffset, lerp);
        rotDisplacement = Quaternion.Slerp(originalRot, fallRotation, lerp);
    }

    public bool IsDone() => currentTime >= time;
}

