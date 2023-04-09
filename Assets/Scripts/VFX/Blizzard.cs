using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blizzard : MonoBehaviour
{
    public float Radius => influenceRadius;
    public float Influence => influence;
    private float influence;

    [SerializeField] private List<PushableObject> pushables = new List<PushableObject>();

    [SerializeField] private float strength;
    [SerializeField] private float influenceRadius;
    [SerializeField] private float influenceThickness;

    public static Blizzard Instance { get; private set; }

    private void Awake() => Instance = this;

    private void Update()
    {
        foreach (PushableObject pushable in pushables)
            PushAway(pushable);
    }

    public void PushAway(PushableObject pushable)
    {
        CalculateInfluence(pushable.transform.position);
        Vector3 forceDirection = GetForceDirection(pushable.transform.position);
        Vector3 force = (forceDirection * influence) * strength;
        pushable.ApplyForce(force);
    }

    private void CalculateInfluence(Vector3 point)
    {
        float distanceToTheCenter = Vector3.Distance(transform.position, point);
        float distanceFromTheInfluenceRadius = Mathf.Max(0f, (distanceToTheCenter - influenceRadius));
        float rawInfluence = distanceFromTheInfluenceRadius / influenceThickness;
        influence = Mathf.Clamp(rawInfluence, 0f, 1f);
        influence = Easing.QuadEaseIn(influence);
    }

    private Vector3 GetForceDirection(Vector3 point)
    {
        Vector3 faceVector = (transform.position - point).FlatV3();
        faceVector = faceVector.normalized;

        return faceVector;
    }
}
