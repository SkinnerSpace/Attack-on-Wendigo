using UnityEngine;

public class Walk : Command
{
    private CharacterData data;
    private IController controller;

    private void Awake()
    {
        data = transform.parent.GetComponent<CharacterData>();
        controller = transform.parent.GetComponent<IController>();
    }

    public override void Execute()
    {
        Vector2 rawVector = controller.GetRawMovementVector();

        Vector3 leftRight = transform.right * rawVector.x;
        Vector3 backwardForward = transform.forward * rawVector.y;
        Vector3 velocity = (leftRight + backwardForward).normalized * data.walkSpeed;

        data.velocity = new Vector3(velocity.x, data.velocity.y, velocity.z);
    }
}
