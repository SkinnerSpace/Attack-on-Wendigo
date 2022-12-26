using UnityEngine;

public class Rotator
{
    private const float viewAngle = 15f;

    private readonly ITransformProxy transform;
    private readonly IClock clock;
    private float speed;

    public Rotator(ITransformProxy transform, IClock clock, float speed)
    {
        this.transform = transform;
        this.clock = clock;
        this.speed = speed;
    }

    public void RotateTo(Vector3 targetPosition)
    {
        Vector3 currentDirection = CalculateCurrentDirection();
        Vector3 expectedDirection = CalculateExpectedDirection(targetPosition);

        float signedSimilarity = (currentDirection.x * expectedDirection.x) + (currentDirection.z * expectedDirection.z);
        float sign = (signedSimilarity / Mathf.Abs(signedSimilarity));
        float relation = CalculateRelation(expectedDirection);
        float expectedAngle = transform.Angle.y + (speed * sign * clock.Delta);

        if (expectedAngle >= 360f)
            expectedAngle = 0f;

        if (expectedAngle < 0f)
            expectedAngle = 360f;

        transform.Angle = new Vector3(0f, expectedAngle, 0f);
        Debug.Log("relation " + relation + " sign " + sign + " angle " + expectedAngle);

        if (relation >= viewAngle)
        {
            //transform.Angle = new Vector3(0f, expectedAngle, 0f);
            
        }

        /*
         * 
         * //float similarity = (Mathf.Abs(currentDirection.x) * Mathf.Abs(expectedDirection.x)) + (Mathf.Abs(currentDirection.z) * Mathf.Abs(expectedDirection.z));
         * 
        Vector3 targetAngle = CalculateAngle(expectedDirection);
        transform.Angle = Vector3.Lerp(transform.Angle, targetAngle, speed * clock.Delta);
        */

        
        //Debug.Log("Current angle " + transform.Angle.y);
        


        //Debug.Log("Relation " + relation);

        Debug.DrawRay(targetPosition, new Vector3(0f, 100f, 0f), Color.red);
        Debug.DrawRay(transform.Position + new Vector3(0f, 20f, 0f), expectedDirection * 100f, Color.red);

        //Debug.Log("Current " + currentDirection);

        //float signedSimilarity = Mathf.Abs(targetAngle.y) / Mathf.Abs(transform.Angle.y);
        //Debug.Log("Similarity " + signedSimilarity);

        //Debug.Log("Current " + transform.Angle + " expected " + targetAngle);
    }

    public Vector3 CalculateCurrentDirection()
    {
        float angle = transform.Angle.y / Mathf.Rad2Deg;
        float x = Mathf.Cos(angle);
        float z = Mathf.Sin(angle);

        Vector3 currentDirection = new Vector3(x, 0f, z);

        return currentDirection;
    }

    public Vector3 CalculateExpectedDirection(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.Position).normalized;
        return direction;
    }

    public float CalculateRelation(Vector3 direction)
    {
        float angleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        //angleY -= (transform.Angle.y);

        /*
        if (angleY < 0f)
            angleY = 360f + angleY;
        */

        float relation = Mathf.Abs(angleY);

        return relation;
    }
}

