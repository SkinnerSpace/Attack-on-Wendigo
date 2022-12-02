using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Hookshot : MonoBehaviour
{
    private float REACH_DISTANCE = 1f;
    private const float PULL_SPEED_MULTIPLIER = 2f;

    [SerializeField] private CharacterData data;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Rope rope;
    [SerializeField] private Character character;
    private IController controller;

    private HookshotTarget hookshotTarget;

    [SerializeField] private float throwSpeed = 70f;
    [SerializeField] private float minPullSpeed = 10f;
    [SerializeField] private float maxPullSpeed = 40f;

    [SerializeField] public Transform debugHitPointTransform;

    private void Awake()
    {
        controller = GetComponent<IController>();
        hookshotTarget = new HookshotTarget();

        rope.gameObject.SetActive(false);
    }

    public void TryThrow()
    {
        if (controller.GetHookshot())
        {
            DetectHookshotTarget();
            ThrowIfTargetExist();
        }
    }

    private void DetectHookshotTarget()
    {
        hookshotTarget.valid = false;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit raycastHit))
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
            rope.SetActive(true);

            character.SetState(State.HookshotThrown);
        }
    }

    public void Fly()
    {
        StretchRope();

        if (rope.length >= GetDistanceToTarget())
        {
            character.SetState(State.HookshotFlying);
            cameraFov.SetCameraFov(HOOKSHOT_VOF);
        }
    }

    private void StretchRope()
    {
        rope.LookAt(hookshotTarget.position);
        rope.Lengthen(throwSpeed);
    }

    public void Movement()
    {
        Pull();
        StopWhenReached();
        

        if (controller.GetHookshot())
        {
            StopHookshot();
        }

        if (controller.GetJump())
        {
            float momentumExtraSpeed = 7f;
            characterVelocityMomentum = hookshotDir * pullSpeed * momentumExtraSpeed;
            float jumpSpeed = 40f;
            characterVelocityMomentum += Vector3.up * jumpSpeed;
            StopHookShot();
        }
    }

    private void Pull()
    {
        rope.LookAt(hookshotTarget.position);
        Vector3 dirToTarget = (hookshotTarget.position - transform.position).normalized;
        float pullSpeed = GetPullSpeed();
        Vector3 pullVelocity = dirToTarget * pullSpeed;
        data.characterController.Move(pullVelocity * Time.deltaTime);
    }

    private void StopWhenReached()
    {
        if (GetDistanceToTarget() <= REACH_DISTANCE)
        {
            StopHookshot();
        }
    }

    public void StopHookshot()
    {
        state = State.Normal;
        ResetGravityEffect();
        rope.SetActive(false);
        cameraFov.SetCameraFov(NORMAL_FOV);
    }

    private float GetDistanceToTarget()
    {
        return Vector3.Distance(transform.position, hookshotTarget.position);
    }

    private float GetPullSpeed()
    {
        float pullSpeed = GetDistanceToTarget();
        pullSpeed = Mathf.Clamp(pullSpeed, minPullSpeed, maxPullSpeed) * PULL_SPEED_MULTIPLIER;

        return pullSpeed;
    }
}