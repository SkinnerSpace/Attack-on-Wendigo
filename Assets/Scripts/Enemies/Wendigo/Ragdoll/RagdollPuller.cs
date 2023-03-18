using System.Collections;
using UnityEngine;

public class RagdollPuller : MonoBehaviour
{
    [SerializeField] private Vector3 direction;

    [SerializeField] private float minForce;
    [SerializeField] private float maxForce;

    [SerializeField] private float minTime = 1f;
    [SerializeField] private float maxTime = 2f;

    private float force;
    private float currentTime;

    private Rigidbody body;
    private bool isPulling;

    private float randomMaxForce;
    private float randomTime;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        randomMaxForce = Rand.Range(minForce, maxForce);
        randomTime = Rand.Range(minTime, maxTime);
    }

    public void Launch()
    {
        force = randomMaxForce;
        isPulling = true;
    }

    private void Update() => Pull();

    public void Pull()
    {
        if (isPulling)
        {
            currentTime += OldChronos.DeltaTime;

            float forcePercent = Mathf.InverseLerp(0f, randomTime, currentTime);
            force = Mathf.Lerp(randomMaxForce, 0f, forcePercent);
            body.velocity = direction * force;

            StopPulling();
        }
    }

    private void StopPulling() => isPulling = currentTime < randomTime;
}