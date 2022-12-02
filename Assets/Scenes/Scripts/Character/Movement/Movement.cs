using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterData data;
    [NonSerialized] public CharacterController body;

    private FlatMovement flatMovement;
    private VerticalMovement verticalMovement;
    private Momentum momentum;

    private void Awake()
    {
        data = GetComponent<CharacterData>();
        body = GetComponent<CharacterController>();

        flatMovement = GetComponent<FlatMovement>();
        verticalMovement = GetComponent<VerticalMovement>();
        momentum = GetComponent<Momentum>();
    }

    public void Process()
    {
        Vector3 flatVelocity = flatMovement.Calculate(data.moveSpeed);
        Vector3 velocity = verticalMovement.ApplyTo(flatVelocity);
        data.velocity = momentum.ApplyTo(velocity);
        momentum.Decelerate();

        body.Move(data.velocity * Time.deltaTime);
    }
}
