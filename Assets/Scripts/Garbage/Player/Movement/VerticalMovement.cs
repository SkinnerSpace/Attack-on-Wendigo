using UnityEngine;

public abstract class VerticalMovement : Command
{
    protected CharacterData data;
    protected LayerMask groundLayer;

    public virtual void Awake()
    {
        data = transform.parent.GetComponent<CharacterData>();
        groundLayer = 1 << 8;
    }

    protected bool IsGrounded()
    {
        return (Physics.CheckSphere(transform.position, 0.1f, groundLayer)) && data.velocity.y <= 0f;
    }
}