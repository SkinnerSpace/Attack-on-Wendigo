using UnityEngine;

public class CratePhysics : MonoBehaviour
{
    private const float REST_VELOCITY = 0.01f;

    [SerializeField] private Rigidbody body;
    [SerializeField] private Collider collisionBox;

    public void SwitchOn(){
        collisionBox.enabled = true;
        ResetKinematic();
    }

    public void SwitchOff()
    {
        collisionBox.enabled = false;
        SetKinematic();
    }

    public void ResetKinematic(){
        body.isKinematic = false;
        body.useGravity = true;
    }

    public void SetKinematic(){
        body.isKinematic = true;
        body.useGravity = false;
        SetVelocity(Vector3.zero);
    }

    public bool IsAtRest() => body.velocity.magnitude <= REST_VELOCITY;

    public void SetVelocity(Vector3 velocity) => body.velocity = velocity;

    public void AddForce(float force) => body.AddForce(transform.forward * force, ForceMode.Impulse);
}
