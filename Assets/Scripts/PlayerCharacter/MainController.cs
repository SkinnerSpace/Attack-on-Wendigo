﻿using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] private MainInputReader mainInputReader;
    [SerializeField] private CharacterData data;
    [SerializeField] private CharacterController controller;
    [SerializeField] private FunctionTimer timer;
    [SerializeField] private Chronos chronos;
    
    private MovementController movementController;
    private JumpController jumpController;
    private DashController dashController;

    private GroundDetectorController groundDetector;
    private SurfaceDetector surfaceDetector;
    private SurfaceStompHandler stompHandler;

    private GravityController gravityController;
    private DecelerationController decelerationController;
    private CameraController cameraController;
    private DampedSpring dampingSpring;

    private FOVController fOVController;
    private CameraTiltController tiltController;

    private void Awake()
    {
        cameraController = new CameraController(data, chronos);
        movementController = new MovementController(data, chronos);
        jumpController = new JumpController(data);
        dashController = new DashController(data, chronos, timer);

        groundDetector = new GroundDetectorController(data, new GroundDetector());
        surfaceDetector = new SurfaceDetector(data, new SurfaceProbeTaker());
        stompHandler = new SurfaceStompHandler(data);

        gravityController = new GravityController(data, chronos);
        decelerationController = new DecelerationController(data, chronos);

        dampingSpring = new DampedSpring(data, chronos);
        fOVController = new FOVController(data, chronos);
        tiltController = new CameraTiltController(data, chronos);

        groundDetector.Subscribe(dampingSpring);
        groundDetector.Subscribe(surfaceDetector);
        groundDetector.Subscribe(gravityController);
        groundDetector.Subscribe(jumpController);
        surfaceDetector.Subscribe(stompHandler);
    }

    private void Start() => ConnectInputReaders();

    private void ConnectInputReaders()
    {
        mainInputReader.GetInputReader<CameraInputReader>().Subscribe(cameraController);

        mainInputReader.GetInputReader<MovementInputReader>().Subscribe(movementController);
        mainInputReader.GetInputReader<MovementInputReader>().Subscribe(dashController);
        mainInputReader.GetInputReader<MovementInputReader>().Subscribe(tiltController);

        mainInputReader.GetInputReader<JumpInputReader>().Subscribe(jumpController);
        mainInputReader.GetInputReader<DashInputReader>().Subscribe(dashController);
    }

    private void Update()
    {
        groundDetector.Update();
        gravityController.ApplyGravity();
        fOVController.Update();
        decelerationController.Decelerate();
        dampingSpring.Update();

        data.CameraRotation = Quaternion.Euler(data.CameraViewEuler + data.CameraTiltEuler);
    }

    private void FixedUpdate()
    {
        controller.Move(data.Velocity * chronos.DeltaTime);
    }
}

