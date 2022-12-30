using UnityEngine;

public class Move : Command, IPushable
{
    private CharacterData data;
    private CharacterController body;
    public Vector3 position => data.transform.position;

    private void Awake()
    {
        data = transform.parent.GetComponent<CharacterData>();
        body = transform.parent.GetComponent<CharacterController>();
    }

    public override void Execute()
    {
        body.Move(data.velocity * Time.deltaTime);
    }

    public void Push(Vector3 pushVelocity)
    {
        body.Move(pushVelocity * Time.deltaTime);
    }
}
