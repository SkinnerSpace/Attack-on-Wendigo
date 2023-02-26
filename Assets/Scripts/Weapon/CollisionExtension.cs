using UnityEngine;

public static class CollisionExtension
{
    public static Vector3 GetHitPoint(this Collision collision)
    {
        Vector3 hitPoint = collision.contacts[0].point;

        foreach (ContactPoint contact in collision.contacts)
            hitPoint = hitPoint.Average(contact.point);

        return hitPoint;
    }
}

