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
        public WendigoSFXPlayer SFXPlayer => sFXPlayer;
        public CharacterController Controller => controller;
        public FunctionTimer Timer => timer;
        public WendigoData Data => data;

        public FireballSpawner FireballSpawner => fireballSpawner;
        public Firebreath Firebreath => firebreath;

        public IChronos Chronos => chronos;
        public IStateMachine stateMachine { get; set; } = NullStateMachine.Instance;
        public IHitBox[] HitBoxes { get; private set; }

        private List<WendigoBaseController> controllers;

        public void OnSpawn() => Data.IsActive = true;
        private void Update()
        {
            if (chronos.IsTicking)
                stateMachine.Tick();
        }

        private void Awake()
        {
            transform.name = "Wendigo_" + (id++);

            //Data = new WendigoData(serializableData, transform);
            HitBoxes = GetComponentsInChildren<IHitBox>();
            mover.Initialize(this);
            AddControllers();

            stateMachine = WendigoStateMachineFactory.Create(this);

            GetController<WendigoMovementController>().Subscribe(GetController<WendigoAnimationController>());
            GetController<WendigoHealthSystem>().SubscribeOnRagdoll(TriggerRagdoll);
            pool.Subscribe(this);
        }

        private void AddControllers()
        {
            controllers = new List<WendigoBaseController>();

            AddController(typeof(WendigoRotationController));
            AddController(typeof(WendigoMovementController));
            AddController(typeof(WendigoHealthSystem));
            AddController(typeof(WendigoTargetManager));
            AddController(typeof(WendigoAnimationController));
            AddController(typeof(WendigoRangeCombatManager));
        }

        private void AddController(Type type)
        {
            WendigoBaseController controller = Activator.CreateInstance(type) as WendigoBaseController;
            controller.Initialize(this);
            controllers.Add(controller);
        }

        public T GetController<T>() where T : WendigoBaseController
        {
            foreach (WendigoBaseController controller in controllers)
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