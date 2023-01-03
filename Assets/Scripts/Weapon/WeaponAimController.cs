using UnityEngine;

public class WeaponAimController : MonoBehaviour
{
    private static float aimCompleteness;

    [SerializeField] private float aimingSpeed = 10f;
    [SerializeField] private float distanceOffset = -0.5f;

    private Vector3 defaultAimingPosition;

    private void Awake()
    {
        defaultAimingPosition = transform.localPosition;
    }

    public void Aim(bool isAiming)
    {
        float targetState = isAiming ? 1f : 0f;
        aimCompleteness = Mathf.Lerp(aimCompleteness, targetState, aimingSpeed * Time.deltaTime);

        Vector3 focusedAimingPosition = new Vector3(0f, defaultAimingPosition.y, defaultAimingPosition.z + distanceOffset);
        transform.localPosition = Vector3.Lerp(defaultAimingPosition, focusedAimingPosition, aimCompleteness);
    }

    public static float GetStability(float stabilityModifier)
    {
        return 1f - (aimCompleteness * stabilityModifier);
    }
}

