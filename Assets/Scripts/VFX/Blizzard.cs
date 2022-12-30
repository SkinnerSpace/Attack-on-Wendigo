using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blizzard : MonoBehaviour
{
    private List<IPushable> insiders = new List<IPushable>();

    [SerializeField] private float pushPower;
    [SerializeField] private float pushRadius;
    [SerializeField] private float pushBorder;
    [SerializeField] private bool update;

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
        foreach (IPushable insider in insiders)
            PushAway(insider);

        if (update)
        {
            squaredPushRadius = pushRadius * pushRadius;
            squaredPushBorder = pushBorder * pushBorder;
        }
    }

    public void AddInsider(IPushable insider)
    {
        insiders.Add(insider);
    }

    public void PushAway(IPushable pushable)
    {
        float squaredDist = CalculateSquaredDist(pushable.position);
        float pushCoefficient = CalculateRepulsionCoefficient(squaredDist);

        if (pushCoefficient > 0)
            ApplyPower(pushable, pushCoefficient);
    }

    public void ApplyPower(IPushable pushable, float pushCoefficient)
    {
        Vector3 pushDirection = (transform.position - pushable.position).normalized;
        Vector3 pushVelocity = pushDirection * pushPower * pushCoefficient;

        pushable.Push(pushVelocity);
    }

    public float CalculateSquaredDist(Vector3 position)
    {
        Vector3 relativePos = position - transform.position;
        float squaredDist = (relativePos.x * relativePos.x) + (relativePos.z * relativePos.z);
        return squaredDist;
    }

    public float CalculateRepulsionCoefficient(float squaredDist)
    {
        float coefficient = (squaredDist - squaredPushRadius) / squaredPushBorder;
        coefficient = Mathf.Max(0f, coefficient);

        return coefficient;
    }

    # if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, pushRadius);
    }
    #endif
}
