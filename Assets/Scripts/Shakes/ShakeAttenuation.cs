using UnityEngine;

public class ShakeAttenuation
{
    public float Amount
    {
        get{
            if (subject != null)
                amount = CalculateAttenuation(subject.position);

            return amount;
        }
    }

    private Transform subject;

    private float amount;
    private Vector3 position;
    private float maxDistance;

    public ShakeAttenuation(float amount) => this.amount = amount;

    public ShakeAttenuation(Vector3 position, float maxDistance)
    {
        this.position = position;
        this.maxDistance = maxDistance;
    }

    public ShakeAttenuation(Vector3 position, Transform subject, float maxDistance) => SetUp(position, subject, maxDistance);

    public void SetUp(Vector3 position, Transform subject, float maxDistance)
    {
        this.position = position;
        this.subject = subject;
        this.maxDistance = maxDistance;
    }

    public float CalculateAttenuation(Vector3 subjectPosition)
    {
        float distance = Vector3.Distance(subjectPosition, position);
        float attenuation = Mathf.InverseLerp(maxDistance, 0f, distance);
        attenuation = (attenuation > 0f) ? Mathf.Pow(attenuation, 2f) : 0f;

        return attenuation;
    }
}
