using UnityEngine;
using System;
using System.Collections.Generic;

public class MainController : MonoBehaviour
{
    [SerializeField] private CharacterData data;
    [SerializeField] private CharacterMover mover;
    [SerializeField] private FunctionTimer timer;
    [SerializeField] private Chronos chronos;

    public CharacterData Data => data;
    public CharacterMover Mover => mover;
    public FunctionTimer Timer => timer;
    public Chronos Chronos => chronos;

    private List<BaseController> controllers;
    private event Action onConnectControlles;

    private void Awake()
    {
        controllers = new List<BaseController>();

        AddController(typeof(MovementController));
        AddController(typeof(JumpController));
        AddController(typeof(DashController));
        AddController(typeof(CameraController));
        AddController(typeof(FOVController));
        AddController(typeof(CameraTiltController));
        AddController(typeof(VisionDetector));
        AddController(typeof(GroundDetector));
        AddController(typeof(SurfaceDetector));
        AddController(typeof(StompHandler));
        AddController(typeof(GravityController));
        AddController(typeof(DecelerationController));
        AddController(typeof(DampedSpring));
        AddController(typeof(PickUpController));
    }

    private void AddController(Type type)
    {
        BaseController controller = Activator.CreateInstance(type) as BaseController;
        controller.Initialize(this);
        controllers.Add(controller);
        onConnectControlles += controller.Connect;
    }

    public T GetController<T>() where T : BaseController
    {
        foreach (BaseController controller in controllers)
        {
            if (controller is T) return controller as T;
        }

        return null;
    }

    private void Start() => onConnectControlles?.Invoke();
}
