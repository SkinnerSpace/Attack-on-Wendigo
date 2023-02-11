using UnityEngine;

public class JumpHandlerBehavior : MonoBehaviour
{
    private JumpController jumpController;
    private GravityController gravityController;

    [SerializeField] private CharacterData data;
    [SerializeField] private JumpInputReader jumpInputReader;
    [SerializeField] private CharacterController controller;

    private void Awake()
    {
        jumpController = new JumpController(data);
        jumpInputReader.Subscribe(jumpController);

        gravityController = new GravityController(data);
    }

    private void Update()
    {
        gravityController.ApplyGravity();
        controller.Move(data.Velocity);
    }
}