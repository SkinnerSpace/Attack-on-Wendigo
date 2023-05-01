using UnityEngine;

public class DrugsPositionFix : MonoBehaviour
{
    [SerializeField] private Pickable pickable;
    [SerializeField] private Vector3 idlePosition;
    [SerializeField] private Vector3 idleAngle;
    [SerializeField] private float transitionTime;

    private Vector3 originalPosition;
    private Vector3 targetPosition;

    private Vector3 originalAngle;
    private Vector3 targetAngle;

    private float time;
    private float lerp;

    private bool isActive;

    private void Awake()
    {
        pickable.onPickedUp += SetHeldPosition;
        pickable.onDropped += SetIdlePosition;
    }

    private void Update()
    {
        if (isActive)
        {
            CountDown();
            UpdatePositionAndRotation();
        }
    }

    private void CountDown()
    {
        time += Time.deltaTime;
        if (time >= transitionTime)
        {
            isActive = false;
        }

        lerp = Mathf.InverseLerp(0f, transitionTime, time);
        lerp = Easing.QuadEaseInOut(lerp);
    }

    private void UpdatePositionAndRotation()
    {
        transform.localPosition = Vector3.Lerp(originalPosition, targetPosition, lerp);
        transform.localEulerAngles = Vector3.Lerp(originalAngle, targetAngle, lerp);
    }

    private void SetIdlePosition()
    {
        SaveOriginalValues();

        targetPosition = idlePosition;
        targetAngle = idleAngle;

        time = 0f;
        isActive = true;
    }

    private void SetHeldPosition()
    {
        SaveOriginalValues();

        targetPosition = Vector3.zero;
        targetAngle = Vector3.zero;

        time = 0f;
        isActive = true;
    }

    private void SaveOriginalValues()
    {
        originalPosition = transform.localPosition;
        originalAngle = transform.localEulerAngles;
    }
}