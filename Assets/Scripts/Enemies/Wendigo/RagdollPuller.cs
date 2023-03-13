using System.Collections;
using UnityEngine;

public class RagdollPuller : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float maxForce;

    private float force;

    private float timeOut = 2f;
    private float currentTime;

    private Rigidbody body;
    private bool isPulling;

    private void Awake() => body = GetComponent<Rigidbody>();

    public void Launch()
    {
        force = maxForce;
        isPulling = true;
    }

    private void Update() => Pull();

    public void Pull()
    {
        if (isPulling)
        {
            Debug.Log("PULL");
            currentTime += OldChronos.DeltaTime;

            float forcePercent = Mathf.InverseLerp(0f, timeOut, currentTime);
            force = Mathf.Lerp(maxForce, 0f, forcePercent);
            body.velocity = direction * force;

            StopPulling();
        }
    }

    private void StopPulling() => isPulling = currentTime < timeOut;
}