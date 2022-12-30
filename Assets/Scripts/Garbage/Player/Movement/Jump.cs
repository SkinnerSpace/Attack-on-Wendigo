using UnityEngine;

public class Jump : VerticalMovement
{
    private IController controller;

    public override void Awake()
    {
        base.Awake();
        controller = transform.parent.GetComponent<IController>();
    }

    public override void Execute()
    {
        if (IsGrounded())
        {
            float verticalVelocity = controller.JumpButtonIsPressed() ? data.jumpStrength : 0f;
            data.velocity = new Vector3(data.velocity.x, verticalVelocity, data.velocity.z);
        }
    }
}
