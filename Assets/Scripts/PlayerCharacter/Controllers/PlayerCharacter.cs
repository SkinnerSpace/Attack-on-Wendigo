using UnityEngine;
using System;
using System.Collections.Generic;

namespace Character
{
    public class PlayerCharacter : MonoBehaviour, ISwitchable
    {
        [SerializeField] private CharacterData oldData;
        [SerializeField] private Data data;
        [SerializeField] private CharacterMover mover;
        [SerializeField] private HitBoxProxy hitBox;
        [SerializeField] private FireHitBox fireHitBox;
        [SerializeField] private FunctionTimer timer;
        [SerializeField] private Chronos chronos;
        [SerializeField] private MainInputReader inputReader;

        public CharacterData OldData => oldData;
        public Data Data => data;
        public CharacterMover Mover => mover;
        public FunctionTimer Timer => timer;
        public Chronos Chronos => chronos;
        public MainInputReader InputReader => inputReader;
        public HitBoxProxy HitBox => hitBox;
        public FireHitBox FireHitBox => fireHitBox;

        private List<BaseController> controllers;
        private event Action onConnectControlles;
        private event Action onDisconnectControllers;

        private void Start()
        {
            GameEvents.current.onIntroIsOver += SwitchOn;
            HelicopterEvents.current.onBoarded += SwitchOff;
            PlayerEvents.current.onDeath += SwitchOff;
        }

        private void Awake()
        {
            controllers = new List<BaseController>();

            mover.Initialize(this);
            onConnectControlles += mover.Connect;

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
            AddController(typeof(InteractionController));
            AddController(typeof(CharacterHealthSystem));
            AddController(typeof(ImpactReceiver));
            AddController(typeof(ScreenshakesController));
            AddController(typeof(CharacterBurnController));
        }

        private void AddController(Type type)
        {
            BaseController controller = Activator.CreateInstance(type) as BaseController;
            controller.Initialize(this);
            controllers.Add(controller);

            onConnectControlles += controller.Connect;
            onDisconnectControllers += controller.Disconnect;
        }

        public T GetController<T>() where T : BaseController
        {
            foreach (BaseController controller in controllers)
            {
                if (controller is T) return controller as T;
            }

            return null;
        }

        public void SwitchOn() => SetActive(true);
        public void SwitchOff() => SetActive(false);

        public void SetActive(bool isActive)
        {
            if (isActive){
                onConnectControlles?.Invoke();
            }

            if (!isActive) 
                onDisconnectControllers?.Invoke();
        }
    }
}
