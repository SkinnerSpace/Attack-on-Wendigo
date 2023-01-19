using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blizzard : MonoBehaviour
{
    private List<IPushable> pushables = new List<IPushable>();

    [SerializeField] private float pushPower;
    [SerializeField] private float pushRadius;
    [SerializeField] private float pushBorder;
    [SerializeField] private bool constantUpdate;

    private float squaredPushRadius;
    private float squaredPushBorder;

    public static Blizzard Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        squaredPushRadius = pushRadius * pushRadius;
        squaredPushBorder = pushBorder * pushBorder;
    }

    private void Update()
    {
        foreach (IPushable pushable in pushables)
            PushAway(pushable);

        if (constantUpdate)
        {
            squaredPushRadius = pushRadius * pushRadius;
            squaredPushBorder = pushBorder * pushBorder;
        }
    }

    public void AddPushable(IPushable pushable) => pushables.Add(pushable);

    public void PushAway(IPushable pushable)
    {
        float proximity = GetProximity(pushable.position);

        float resistance = GetResistance(pushable, proximity);
        pushable.SetResistance(resistance);

        Vector3 force = GetForce(pushable, proximity);
        pushable.ApplyForce(force);
    }

    public float GetProximity(Vector3 position)
    {
        float squaredDist = GetSquaredDist(position);
        float rawProximity = (squaredDist - squaredPushRadius) / squaredPushBorder;
        float proximity = Mathf.Clamp(rawProximity, 0f, 1f);

        return proximity;
    }

    public float GetSquaredDist(Vector3 position)
    {
        Vector3 relativePos = position - transform.position;
        float squaredDist = (relativePos.x * relativePos.x) + (relativePos.z * relativePos.z);

        return squaredDist;
    }

    private float GetResistance(IPushable pushable, float proximity)
    {
        float directedness = GetDirectedness(pushable);
        float resistance = 1f - (proximity * directedness);

        return resistance;
    }

    private float GetDirectedness(IPushable pushable)
    {
        Vector3 faceVector = (transform.position - pushable.position);
        Vector2 flatFaceVector = new Vector2(faceVector.x, faceVector.z);
        Vector2 faceDirection = flatFaceVector.normalized;

        Vector2 currentDirection = new Vector2(pushable.direction.x, pushable.direction.z).normalized;

        float directedness = Vector2.Dot(faceDirection, currentDirection);
        directedness = -directedness; //Mathf.Max(0f, -directedness);
       // directedness = Mathf.Sqrt(directedness);

        return directedness;
    }

    public Vector3 GetForce(IPushable pushable, float proximity)
    {
        Vector3 direction = (transform.position - pushable.position).normalized;
        Vector3 force = direction * pushPower * proximity;

        return force;
    }
}
