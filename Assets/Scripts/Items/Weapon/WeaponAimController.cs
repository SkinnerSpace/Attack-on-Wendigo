using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAimController : MonoBehaviour, IAimController
{
    private enum TestModes
    {
        Off,
        Center,
        Default,
        Aiming
    }

    public Vector3 DefaultPosition => defaultPosition;

    [Header("Required Components")]
    [SerializeField] private WeaponData data;

    [Header("Settings")]
    [SerializeField] private Vector3 defaultPosition;
    [SerializeField] private Vector3 aimingPosition;

    [Header("Test")]
    [SerializeField] private TestModes testMode = TestModes.Off;

    private bool isAiming;
    private float focus;

    private float transitionTime = 0.2f;
    private float currentTime;

    public void Aim(bool isAiming)
    {
        this.isAiming = isAiming;
        data.SetPrecisionAdjustrment(isAiming ? data.AimPrecision : 0f);

        StopCoroutine(MakeTransition());
        StartCoroutine(MakeTransition());
    }

    private IEnumerator MakeTransition()
    {
        while (IsMakingTransition)
        {
            currentTime = isAiming ? (currentTime += OldChronos.DeltaTime) : (currentTime -= OldChronos.DeltaTime);
            ChangePosition();

            yield return null;
        }
    }

    private bool IsMakingTransition => (isAiming && currentTime < transitionTime) || (!isAiming && currentTime > 0f);

    private void ChangePosition()
    {
        currentTime = Mathf.Clamp(currentTime, 0f, transitionTime);
        focus = (currentTime / transitionTime).QuadEaseInOut();
        transform.localPosition = Vector3.Lerp(defaultPosition, aimingPosition, focus);
    }

    public float GetStability(float stabilityModifier) => 1f - (focus * stabilityModifier);
    private void SetPosition(Vector3 position) => transform.localPosition = position;

    private void OnDrawGizmos() => Test();

    private void Test()
    {
        switch (testMode)
        {
            case TestModes.Center:
                SetPosition(Vector3.zero); break;

            case TestModes.Default:
                Aim(false); break;

            case TestModes.Aiming:
                Aim(true); break;
        }
    }
}

