using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blizzard : MonoBehaviour
{
    public float Radius => radius;

    private List<IPushable> pushables = new List<IPushable>();

    [SerializeField] private float strength;
    [SerializeField] private float radius;
    [SerializeField] private float maxOuterDistance;

    public static Blizzard Instance { get; private set; }

    private void Awake() => Instance = this;

    private void Update()
    {
        foreach (IPushable pushable in pushables)
            PushAway(pushable);
    }

    public void AddPushable(IPushable pushable) => pushables.Add(pushable);

    public void PushAway(IPushable pushable)
    {
        float proximity = GetProximity(pushable.position);

        float resistance = GetResistance(pushable, proximity);
        pushable.SetResistance(resistance);

        Vector3 force = GetForce(pushable, proximity);
        //pushable.ApplyForce(force);
    }

    public float GetProximity(Vector3 objPosition)
    {
        float distance = Vector3.Distance(transform.position, objPosition);
        float rawProximity = Mathf.Max(0f, (distance - radius)) / maxOuterDistance;
        float proximity = Mathf.Clamp(rawProximity, 0f, 1f);

        return proximity;
    }

    private float GetResistance(IPushable pushable, float proximity)
    {
        float directedness = GetDirectedness(pushable);
        float resistance = Mathf.Clamp((proximity * directedness), 0f, 1f);
        resistance = 1f - resistance;

        return resistance;
    }

    private float GetDirectedness(IPushable pushable)
    {
        Vector3 faceVector = (transform.position - pushable.position);
        Vector2 flatFaceVector = new Vector2(faceVector.x, faceVector.z);
        Vector2 faceDirection = flatFaceVector.normalized;

        Vector2 currentDirection = new Vector2(pushable.direction.x, pushable.direction.z).normalized;

        float directedness = Vector2.Dot(faceDirection, currentDirection);
        directedness = -directedness;

        return directedness;
    }

    public Vector3 GetForce(IPushable pushable, float proximity)
    {
        Vector3 direction = (transform.position - pushable.position).normalized;
        Vector3 force = direction * strength * proximity;

        return force;
    }
}
