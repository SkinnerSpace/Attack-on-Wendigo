using UnityEngine;

public interface IPhysicalBody
{
    void AddForce(Vector3 force);
    void AddTorque(Vector3 torque);

    void EnablePhysics();

    void DisablePhysics();
}
