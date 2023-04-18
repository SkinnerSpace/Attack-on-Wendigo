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
        [SerializeField] private Animator animator;
        [SerializeField] private WendigoSFXPlayer sFXPlayer;        
        [SerializeField] private WendigoPooledObject poolObject;
        [SerializeField] private Transform limbsRoot;

        [Header("Death Components")]
        [SerializeField] private PropDestroyer mainPropDestroyer;
        [SerializeField] private RagDollController ragDollController;
        [SerializeField] private CorpseCollisionController corpseCollisionController;
        [SerializeField] private WendigoCorpse corpse;

        [Header("Combat")]
        [SerializeField] private FireballSpawnerComponent fireballSpawner;
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
        public WendigoPooledObject PoolObject => poolObject;
        public WendigoData Data => data;

        public FireballSpawnerComponent FireballSpawner => fireballSpawner;
        public Firebreath Firebreath => firebreath;

        public IChronos Chronos => chronos;
        public IStateMachine stateMachine { get; set; } = NullStateMachine.Instance;
        public IHitBox[] HitBoxes { get; private set; }

        private List<WendigoPlugableComponent> controllers;
        public Limb[] limbs { get; private set; }

        private event Action<Transform> notifyOnDeath;
        public event Action onDeath;

        public void OnSpawn()
        {
            Data.IsActive = true;

            if (stateMachine != null){
                stateMachine = WendigoStateMachineFactory.Clear(stateMachine);
            }
        }

        private void Update()
        {
            if (chronos.IsTicking)
                stateMachine.Tick();
        }

        private void Awake()
        {
            AssignName();

            HitBoxes = GetComponentsInChildren<IHitBox>();
            limbs = limbsRoot.GetComponentsInChildren<Limb>();
            AddControllers();

            stateMachine = WendigoStateMachineFactory.Create(this);
            ManageSubscriptions(); 
        }

        private void AssignName() => transform.name = "Wendigo_" + (++id);

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

        private void ManageSubscriptions()
        {
            GetController<WendigoMovementController>().onVelocityUpdate += GetController<WendigoAnimationController>().OnVelocityUpdate;

            GetController<WendigoHealthSystem>().SubscribeOnDeath(OnDeath);
            GetController<WendigoHealthSystem>().SubscribeOnImpactApply(OnImpact);
            GetController<WendigoHealthSystem>().onInjuryDegreeUpdate += GetController<WendigoMovementController>().UpdateSpeed;

            poolObject.SubscribeOnSpawn(OnSpawn);
            poolObject.SubscribeOnSpawn(GetController<WendigoAnimationController>().ResetState);
            poolObject.SubscribeOnSpawn(GetController<WendigoHealthSystem>().OnSpawn);
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

        public void SubscribeOnDeath(Action<Transform> notifyOnDeath) => this.notifyOnDeath += notifyOnDeath;

        public void OnDeath()
        {
            GetController<WendigoTargetManager>().ResetTarget();
            mainPropDestroyer.enabled = false;
            ragDollController.SwitchOn();
            corpseCollisionController.SwitchOn();

            BuryTheCorpse();
            onDeath?.Invoke();
            notifyOnDeath?.Invoke(transform);
        }

        public void OnImpact(Vector3 impact, Vector3 hitPoint){
            ragDollController.ApplyForce(impact, hitPoint);
        }

        public void SetTarget(Transform target)
        {
            GetController<WendigoTargetManager>().SetTarget(target);
        }
        private void BuryTheCorpse() => WendigoCorpseCollector.Instance.AddCorpse(corpse);

        public void BackUp() => data.ResetData();

        public void SetHealth(int healthAmount)
        {
            data.Health.InitialAmount = healthAmount;
            data.Health.Amount = data.Health.InitialAmount;
        }

        public void SetSpeed(float speedMultiplier){
            data.Movement.MULTIPLIER = speedMultiplier;
        }

        public void SetMaxFireballAttackDistance(float maxFireballAttackDistance){
            data.Fireball.MaxDistance = maxFireballAttackDistance;
        }
    }
}

