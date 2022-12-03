using UnityEngine;

public class Hookshot : MonoBehaviour, ICharacterDependee
{
    private float REACH_DISTANCE = 1f;
    private const float PULL_SPEED_MULTIPLIER = 2f;

    private Character character;

    private Rope rope;
    private HookshotTarget hookshotTarget;

    [SerializeField] private float throwSpeed = 70f;
    [SerializeField] private float minPullSpeed = 10f;
    [SerializeField] private float maxPullSpeed = 40f;

    [SerializeField] public Transform debugHitPointTransform;

    private bool active = false;

    private void Update()
    {
        if (!active)
        {
            rope.Shorten(rope.length * 10f);
        }
    }

    public void SetUp(Character character)
    {
        this.character = character;

        rope = GetComponent<Rope>();
        active = false;

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
            active = true;

            character.SetState(State.HookshotThrown);
        }
    }

    public void FlyToTarget()
    {
        StretchRope(throwSpeed);

        if (rope.length >= GetDistanceToTarget())
        {
            character.SetState(State.HookshotFlying);
            character.cameraFov.SetCameraFov(CameraFov.HOOKSHOT_VOF);
        }
    }

    private void StretchRope(float speed)
    {
        rope.LookAt(hookshotTarget.position);
        rope.Lengthen(speed);
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

    private void Pull()
    {
        Vector3 pullVelocity = GetPullVelocity();
        ShrinkRope(pullVelocity.magnitude);
        character.body.Move(pullVelocity * Time.deltaTime);
    }

    private void ShrinkRope(float speed)
    {
        rope.LookAt(hookshotTarget.position);
        rope.Shorten(speed);
    }

    private void StopWhenReached()
    {
        if (GetDistanceToTarget() <= REACH_DISTANCE)
            StopHookshot();
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
        active = false;
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