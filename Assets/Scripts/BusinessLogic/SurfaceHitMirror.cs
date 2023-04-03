using UnityEngine;

public static class SurfaceHitMirror
{
    public static Quaternion ReflectRotation(Vector3 direction, Vector3 normal)
    {
        Vector3 reflectedDirection = Vector3.Reflect(direction, normal);
        reflectedDirection = CorrectDirection(reflectedDirection);
        Quaternion reflectedRotation = Quaternion.LookRotation(reflectedDirection, Vector3.up);

        return reflectedRotation;
    }

    private static Vector3 CorrectDirection(Vector3 direction) => (direction.magnitude <= 0f) ? Vector3.down : direction;
}
