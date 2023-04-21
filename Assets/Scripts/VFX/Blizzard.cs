using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blizzard : MonoBehaviour, IBlizzard
{
    public float Radius => influenceRadius * blizzardTransform.Size;
    public float Influence => influence;
    private float influence;

    [SerializeField] private List<PushableObject> pushables = new List<PushableObject>();
    [SerializeField] private BlizzardTransform blizzardTransform;

    [SerializeField] private float strength;
    [SerializeField] private float influenceRadius;
    [SerializeField] private float influenceThickness;

    [SerializeField] private bool showRadius;

/*    private float differenceFromTheOriginalSize;
*/
    public static Blizzard Instance { get; private set; }

    private void Awake(){
        Instance = this;
        blizzardTransform.onTargetSizeUpdate += NotifyOnRadiusUpdate;
    }

    private void Update()
    {
        foreach (PushableObject pushable in pushables)
            PushAway(pushable);

    /*    differenceFromTheOriginalSize = Radius / influenceRadius;*/
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
        float distanceFromTheInfluenceRadius = Mathf.Max(0f, (distanceToTheCenter - (influenceRadius * blizzardTransform.Size)));
        float rawInfluence = distanceFromTheInfluenceRadius / (influenceThickness * blizzardTransform.Size);
        influence = Mathf.Clamp(rawInfluence, 0f, 1f);
        influence = Easing.QuadEaseIn(influence);
    }

    private Vector3 GetForceDirection(Vector3 point)
    {
        Vector3 faceVector = (transform.position - point).FlatV3();
        faceVector = faceVector.normalized;

        return faceVector;
    }

    private void NotifyOnRadiusUpdate()
    {
        GameEvents.current.UpdateBlizzardRadius(Radius, transform.position);
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if (showRadius)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Radius + (influenceThickness * blizzardTransform.Size));
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, influenceRadius);
            Gizmos.color = Color.white;
        }
    }

# endif
}
