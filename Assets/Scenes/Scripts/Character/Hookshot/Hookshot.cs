using UnityEngine;

public class Hookshot : MonoBehaviour
{
    private float REACH_DISTANCE = 1f;
    private const float PULL_SPEED_MULTIPLIER = 2f;

    [SerializeField] private Character character;

    private Rope rope;
    private HookshotTarget hookshotTarget;

    [SerializeField] private float throwSpeed = 70f;
    [SerializeField] private float minPullSpeed = 10f;
    [SerializeField] private float maxPullSpeed = 40f;

    [SerializeField] public Transform debugHitPointTransform;

    private void Awake()
    {
        rope = GetComponent<Rope>();
        rope.gameObject.SetActive(false);

        hookshotTarget = new HookshotTarget();
    }

    public void Throw()
    {
        if (character.controller.GetHookshot())
        {
            DetectHookshotTarget();
            ThrowIfTargetExist();
        }
    }

    private void DetectHookshotTarget()
    {
        hookshotTarget.valid = false;

        if (Physics.Raycast(character.mainCamera.transform.position, character.mainCamera.transform.forward, out RaycastHit raycastHit))
        {
            hookshotTarget.position = raycastHit.point;
            hookshotTarget.valid = true;
        }
    }

    private void ThrowIfTargetExist()
    {
        if (hookshotTarget.valid)
        {
            debugHitPointTransform.position = hookshotTarget.position;
            rope.SetLength(0f);
            rope.LookAt(hookshotTarget.position);
            rope.SetActive(true);

            character.SetState(State.HookshotThrown);
        }
    }

    public void FlyToTarget()
    {
        StretchRope();

        if (rope.length >= GetDistanceToTarget())
        {
            character.SetState(State.HookshotFlying);
            character.cameraFov.SetCameraFov(CameraFov.HOOKSHOT_VOF);
        }
    }

    private void StretchRope()
    {
        rope.LookAt(hookshotTarget.position);
        rope.Lengthen(throwSpeed);
    }

    public void PullBodyToTarget()
    {
        Pull();
        StopWhenReached();
        
        if (character.controller.GetHookshot())
            StopHookshot();

        if (character.controller.GetJump())
            AirJump();
    }

    private void StopWhenReached()
    {
        if (GetDistanceToTarget() <= REACH_DISTANCE)
            StopHookshot();
    }

    private void Pull()
    {
        rope.LookAt(hookshotTarget.position);
        Vector3 pullVelocity = GetPullVelocity();
        character.body.Move(pullVelocity * Time.deltaTime);
    }

    private void AirJump()
    {
        character.data.velocityMomentum = GetPullVelocity() * character.data.momentumMultiplier;
        character.data.velocityMomentum += Vector3.up * character.data.jumpStrength;
        StopHookshot();
    }

    private Vector3 GetPullVelocity()
    {
        Vector3 dirToTarget = GetDirToTarget();
        float pullSpeed = GetPullSpeed();
        Vector3 pullVelocity = dirToTarget * pullSpeed;

        return pullVelocity;
    }

    public void StopHookshot()
    {
        character.SetState(State.Normal);
        character.data.verticalVelocity = 0f;
        rope.SetActive(false);
        character.cameraFov.SetCameraFov(CameraFov.NORMAL_FOV);
    }

    private float GetDistanceToTarget()
    {
        return Vector3.Distance(transform.position, hookshotTarget.position);
    }

    private Vector3 GetDirToTarget()
    {
        Vector3 hookshotDir = (hookshotTarget.position - transform.position).normalized;
        return hookshotDir;
    }

    private float GetPullSpeed()
    {
        float pullSpeed = GetDistanceToTarget();
        pullSpeed = Mathf.Clamp(pullSpeed, minPullSpeed, maxPullSpeed) * PULL_SPEED_MULTIPLIER;

        return pullSpeed;
    }
}