using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WendigoCharacter
{
    public class Wendigo : MonoBehaviour, IWendigo, IRagdoll, IPooledObjectObserver
    {
        public static int id;

        [SerializeField] private WendigoData data;

        [Header("Required Components")]
        [SerializeField] private WendigoMover mover;
        [SerializeField] private CharacterController controller;
        [SerializeField] private PropDestroyer mainPropDestroyer;
        [SerializeField] private RagDollController ragDollController;
        [SerializeField] private WendigoPooledObject pool;
        [SerializeField] private Animator animator;
        [SerializeField] private WendigoSFXPlayer sFXPlayer;

        [Header("Combat")]
        [SerializeField] private FireballSpawner fireballSpawner;
        [SerializeField] private Firebreath firebreath;

        [Header("Timer")]
        [SerializeField] private FunctionTimer timer;
        [SerializeField] private Chronos chronos;

        [SerializeField] private Transform pendingTarget;
        public Transform PendingTarget => pendingTarget;

        public Animator Animator => animator;
        public WendigoMover Mover => mover;
        public WendigoSFXPlayer SFXPlayer => sFXPlayer;
        public FunctionTimer Timer => timer;
        public WendigoData Data => data;

        public FireballSpawner FireballSpawner => fireballSpawner;
        public Firebreath Firebreath => firebreath;

        public IChronos Chronos => chronos;
        public IStateMachine stateMachine { get; set; } = NullStateMachine.Instance;
        public IHitBox[] HitBoxes { get; private set; }

        private List<WendigoPlugableComponent> controllers;

        public void OnSpawn() => Data.IsActive = true;
        private void Update()
        {
            if (chronos.IsTicking)
                stateMachine.Tick();
        }

        private void Awake()
        {
            transform.name = "Wendigo_" + (id++);

            HitBoxes = GetComponentsInChildren<IHitBox>();
            AddControllers();

            stateMachine = WendigoStateMachineFactory.Create(this);

            GetController<WendigoMovementController>().Subscribe(GetController<WendigoAnimationController>().OnVelocityUpdate);
            GetController<WendigoHealthSystem>().SubscribeOnRagdoll(TriggerRagdoll);
            pool.Subscribe(this);
        }

        private void AddControllers()
        {
            controllers = new List<WendigoPlugableComponent>();

            AddController(typeof(WendigoRotationController));
            AddController(typeof(WendigoMovementController));
            AddController(typeof(WendigoHealthSystem));
            AddController(typeof(WendigoTargetManager));
            AddController(typeof(WendigoAnimationController));
            AddController(typeof(RangeCombatManager));
        }

        private void AddController(Type type)
        {
            WendigoPlugableComponent controller = Activator.CreateInstance(type) as WendigoPlugableComponent;
            controller.Initialize(this);
            controllers.Add(controller);
        }

        public T GetController<T>() where T : WendigoPlugableComponent
        {
            foreach (WendigoPlugableComponent controller in controllers)
            {
                if (controller is T) return controller as T;
            }

            return null;
        }

        public void TriggerRagdoll(Vector3 impact, Vector3 hitPoint)
        {
            SetTarget(null);
            ragDollController.TriggerRagdoll(impact, hitPoint);
        }

        public void SetTarget(Transform target) => GetController<WendigoTargetManager>().SetTarget(target);
    }
}