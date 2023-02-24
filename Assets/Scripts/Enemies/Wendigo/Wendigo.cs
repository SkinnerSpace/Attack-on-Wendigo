using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wendigo : MonoBehaviour, IWendigo, IRagdoll, IPooledObjectObserver
{
    [Header("Required Components")]
    [SerializeField] private PropDestroyer mainPropDestroyer;
    [SerializeField] private RagDollController ragDollController;
    [SerializeField] private WendigoPooledObject pool;

    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;
    [SerializeField] private WendigoMover mover;
    [SerializeField] private WendigoSerializableData serializableData;
    [SerializeField] private FunctionTimer timer;
    [SerializeField] private Chronos chronos;

    public Animator Animator => animator;
    public CharacterController Controller => controller;
    public FunctionTimer Timer => timer;

    public WendigoData Data { get; set; }
    public IChronos Chronos => chronos;

    public WendigoRotationController RotationController { get; set; }
    public WendigoMovementController MovementController { get; set; }
    public WendigoHealthSystem HealthSystem { get; set; }
    public WendigoAnimatorController AnimatorController { get; set; }
    public WendigoTargetManager targetManager { get; set; }
    public IStateMachine stateMachine { get; set; } = NullStateMachine.Instance;

    public IHitBox[] HitBoxes { get; private set; }

    [HideInInspector] public bool testDeath;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        Data = new WendigoData(serializableData, new ProxyTransform(transform));
        HitBoxes = GetComponentsInChildren<IHitBox>();

        InitializeComponents();
        WendigoStateMachineInstaller.SetUp(this, stateMachine);

        pool.Subscribe(this);
        HealthSystem.SubscribeOnRagdoll(this);
    }

    public void OnSpawn() => Data.IsActive = true;

    private void Update() => stateMachine.Tick();

    private void InitializeComponents()
    {
        mover.Initialize(this);

        RotationController = new WendigoRotationController(this);
        MovementController = new WendigoMovementController(this);
        HealthSystem = new WendigoHealthSystem(this);
        targetManager = new WendigoTargetManager(this);
        AnimatorController = new WendigoAnimatorController(this);
        stateMachine = new StateMachine();

        MovementController.Subscribe(AnimatorController);
    }

    public void TriggerRagdoll(Vector3 impact, Vector3 hitPoint)
    {
        SetTarget(null);
        ragDollController.TriggerRagdoll(impact, hitPoint);
        testDeath = true;
    }

    public void SetTarget(Transform target) => targetManager.SetTarget(target);
}
