using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Character character;
    public Character Character => character;

    private FlatMovement flatMovement;
    private VerticalMovement verticalMovement;
    private Momentum momentum;

    private void Awake()
    {
        flatMovement = GetComponent<FlatMovement>();
        verticalMovement = GetComponent<VerticalMovement>();
        momentum = GetComponent<Momentum>();
    }

    public void Move()
    {
        Vector3 flatVelocity = flatMovement.Calculate(character.data.moveSpeed);
        Vector3 velocity = verticalMovement.ApplyTo(flatVelocity);
        character.data.velocity = momentum.ApplyTo(velocity);
        momentum.Decelerate();

        character.body.Move(character.data.velocity * Time.deltaTime);
    }
}
