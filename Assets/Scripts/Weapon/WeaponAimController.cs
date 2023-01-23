using UnityEngine;

public class WeaponAimController : MonoBehaviour
{
    private static float currentState;
    private enum TestModes
    {
        Off,
        Center,
        Default,
        Aiming
    }

    public Vector3 DefaultPosition => defaultPosition;

    [Header("Required Components")]
    [SerializeField] private IPickable pickableItem;

    [Header("Settings")]
    [SerializeField] private Vector3 defaultPosition;
    [SerializeField] private Vector3 aimingPosition;
    [SerializeField] private float aimingSpeed = 10f;

    [Header("Test")]
    [SerializeField] private TestModes testMode = TestModes.Off;

    private Vector3 currentPosition;

    public void Aim(bool isAiming)
    {
        float targetState = isAiming ? 1f : 0f;
        currentState = GetCurrentState(targetState);
        currentPosition = GetCurrentPosition(currentState);
        SetPosition(currentPosition);
    }

    public static float GetStability(float stabilityModifier) => 1f - (currentState * stabilityModifier);
    private float GetCurrentState(float targetState) => Mathf.Lerp(currentState, targetState, aimingSpeed * Chronos.DeltaTime);
    private Vector3 GetCurrentPosition(float currentState) => Vector3.Lerp(defaultPosition, aimingPosition, currentState);
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

