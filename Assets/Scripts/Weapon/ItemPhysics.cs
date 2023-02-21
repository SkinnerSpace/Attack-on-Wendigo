using UnityEngine;

public class ItemPhysics : MonoBehaviour
{
    [SerializeField] private Rigidbody physics;
    [SerializeField] private Collider collision;

    public void ComeToRest() => SetDisabled(true);

    public void ApplyForce(Vector3 force)
    {
        SetDisabled(false);
        physics.AddForce(force);
    }

    private void SetDisabled(bool disabled)
    {
        collision.enabled = !disabled;
        physics.isKinematic = disabled;
        physics.useGravity = !disabled;
    }
}


