using System.Collections;
using UnityEngine;

public class RagdollPuller : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float maxForce;

    private float force;

    private float forceTime = 2f;
    private float currentTime;

    private Rigidbody body;
    private bool isPulling;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    public void Launch()
    {
        force = maxForce;
        isPulling = true;
        //StartCoroutine(Pulling());
    }

    private void Update() => Pull();

    public void Pull()
    {
        if (isPulling)
        {
            currentTime += Chronos.DeltaTime;

            float interpolation = Mathf.InverseLerp(0f, forceTime, currentTime);
            force = Mathf.Lerp(0, maxForce, interpolation);

            body.velocity = direction * force;

            if (currentTime >= forceTime)
            {
                isPulling = false;
            }
        }
    }

    private IEnumerator Pulling()
    {
        while (currentTime < forceTime){
            currentTime += Chronos.DeltaTime;

            float interpolation = Mathf.InverseLerp(0f, forceTime, currentTime);
            force = Mathf.Lerp(maxForce, 0f, interpolation);

            body.velocity = direction * force;
            Debug.Log(body.velocity);

        }

        yield return null;
    }
}