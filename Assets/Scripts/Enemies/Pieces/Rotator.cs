using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform verticalTf;
    [SerializeField] private Transform horizontalTf;
    [SerializeField] private float rotationSpeed;

    [Header("Required components")]
    [SerializeField] private FlyingEnemy character;

    private Quaternion targetRotation;

    private void Awake()
    {
        targetRotation = transform.rotation;
    }

    private void Update()
    {
        if (character.Target != null) SetTargetRotation();
        RotateToTarget();
    }

    private void SetTargetRotation()
    {
        Vector3 targetDir = (character.Target.position - transform.position).normalized;
        targetRotation = Quaternion.LookRotation(targetDir);
    }

    private void RotateToTarget()
    {
        if (verticalTf != null) RotateVertically();
        if (horizontalTf != null) RotateHorizontally();
    }

    private void RotateVertically()
    {
        verticalTf.localRotation = Quaternion.Lerp(verticalTf.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
        Vector3 adjustedAngles = new Vector3(verticalTf.eulerAngles.x, 0f, 0f);
        verticalTf.localEulerAngles = adjustedAngles;
    }

    private void RotateHorizontally()
    {
        horizontalTf.localRotation = Quaternion.Lerp(horizontalTf.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
        Vector3 adjustedAngles = new Vector3(0f, horizontalTf.eulerAngles.y, horizontalTf.eulerAngles.z);
        horizontalTf.localEulerAngles = adjustedAngles;
    }
}