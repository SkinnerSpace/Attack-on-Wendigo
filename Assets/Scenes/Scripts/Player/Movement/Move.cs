using UnityEngine;

public class Move : Command
{
    private CharacterData data;
    private CharacterController body;

    private void Awake()
    {
        data = transform.parent.GetComponent<CharacterData>();
        body = transform.parent.GetComponent<CharacterController>();
    }

    public override void Execute()
    {
        body.Move(data.velocity * Time.deltaTime);
    }
}
