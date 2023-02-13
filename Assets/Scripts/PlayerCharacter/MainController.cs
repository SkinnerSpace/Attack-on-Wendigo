using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] private MainInputReader mainInputReader;
    [SerializeField] private CharacterData data;
    [SerializeField] private CharacterController controller;
    [SerializeField] private GroundDetectorBehavior groundDetector;

    private IChronos chronos;

    private MovementController movementController;
    private JumpController jumpController;
    private DashController dashController;
    private GravityController gravityController;
    private DecelerationController decelerationController;

    private void Awake()
    {
        chronos = new Chronos();

        movementController = new MovementController(data, chronos);
        jumpController = new JumpController(data);
        dashController = new DashController(data, chronos);
        gravityController = new GravityController(data, chronos);
        decelerationController = new DecelerationController(data, chronos);
        
        groundDetector.Subscribe(gravityController);
        groundDetector.Subscribe(jumpController);
    }

    private void Start() => ConnectInputReaders();

    private void ConnectInputReaders()
    {
        mainInputReader.GetInputReader<MovementInputReader>().Subscribe(movementController);
        mainInputReader.GetInputReader<JumpInputReader>().Subscribe(jumpController);
        mainInputReader.GetInputReader<DashInputReader>().Subscribe(dashController);
    }

    private void Update()
    {
        gravityController.ApplyGravity();
        decelerationController.Decelerate();
    }

    private void FixedUpdate()
    {
        controller.Move(data.Velocity * chronos.DeltaTime);
    }
}

