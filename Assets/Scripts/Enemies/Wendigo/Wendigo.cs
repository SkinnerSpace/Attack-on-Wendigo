using System;
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
    [SerializeField] private WendigoData data;
    [SerializeField] private FunctionTimer timer;
    [SerializeField] private Chronos chronos;

    public Animator Animator => animator;
    public CharacterController Controller => controller;
    public FunctionTimer Timer => timer;

    public HitBox[] HitBoxes { get; set; }
    public WendigoData Data => data;
    public IChronos Chronos => chronos;

    public WendigoRotationController RotationController { get; set; }
    public WendigoMovementController MovementController { get; set; }
    public WendigoHealthSystem HealthSystem { get; set; }
    public WendigoAnimatorController AnimatorController { get; set; }
    public StateMachine stateMachine { get; set; }

    [HideInInspector] public bool testDeath;

    private event Action<Transform> onTargetSet;

    private void Awake()
    {
        pool.Subscribe(this);
        HitBoxes = GetComponentsInChildren<HitBox>();
    }

    public void OnSpawn() => Initialize();

    public void Initialize()
    {
        InitializeComponents();
        WendigoStateMachineInstaller.SetUp(this, stateMachine);

        HealthSystem.SubscribeOnRagdoll(this);
    }

    private void Update()
    {
        if (stateMachine != null)
            stateMachine.Tick();
    }

    private void InitializeComponents()
    {
        mover.Initialize(this);

        RotationController = new WendigoRotationController(this);
        MovementController = new WendigoMovementController(this);
        HealthSystem = new WendigoHealthSystem(this);
        AnimatorController = new WendigoAnimatorController(this);

        MovementController.Subscribe(AnimatorController);

        stateMachine = new StateMachine();
    }

    public void TriggerRagdoll(Vector3 impact, Vector3 hitPoint)
    {
        //headTarget.SetTarget(null);
        SetTarget(null);
        ragDollController.TriggerRagdoll(impact, hitPoint);
        testDeath = true;
    }

    public void SetTarget(Transform target)
    {
        Data.Target = target;
        onTargetSet?.Invoke(target);
    }
}
