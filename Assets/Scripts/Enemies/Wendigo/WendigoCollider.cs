using UnityEngine;
using UnityEditor;

namespace WendigoCharacter
{
    public class WendigoCollider : MonoBehaviour, ISwitchable
    {
        private const float UPWARD_IMPACT = 0.5f;

        [SerializeField] private WendigoData data;
        [SerializeField] private Wendigo wendigo;
        [SerializeField] private AnimationEventControllerComponent animationEvents;
        [SerializeField] private WendigoPooledObject pooledObject;
        [SerializeField] private bool visualize;

        private StompColliderData colliderData;
        private PushableObject pushable;
        private IDamageable damageable;

        private bool withinCollisionRadius;
        private bool withinDamageRange;

        private Vector3 direction;
        private float height;

        private bool isActive = true;

        public void SwitchOn() => isActive = true;
        public void SwitchOff() => isActive = false;

        private void Awake(){
            colliderData = data.Collider;
            pooledObject.SubscribeOnSpawn(SwitchOn);
            wendigo.onDeath += SwitchOff;
        }

        private void Start(){
            pushable = data.Target.transform.GetComponent<PushableObject>();
            damageable = data.Target.transform.GetComponentInChildren<IDamageable>();
            animationEvents.onStomp += DamageTheTarget;
        }

        private void Update()
        {
            if (isActive){
                UpdateVerticalDistanceToTheVictim();
                withinCollisionRadius = CheckWithinTheRadius();
                UpdateIfWithinTheCollisionRadius();

                withinDamageRange = withinCollisionRadius && OnTheDamageHeight();
            }
        }

        private void UpdateVerticalDistanceToTheVictim()
        {
            float targetHeight = pushable.transform.position.y;
            float ownHeight = transform.position.y;

            height = Mathf.Abs(targetHeight - ownHeight);
        }

        private bool CheckWithinTheRadius() => Vector2.Distance(transform.position.FlatV2(), pushable.transform.position.FlatV2()) <= colliderData.radius;

        private void UpdateIfWithinTheCollisionRadius()
        {
            if (withinCollisionRadius){
                UpdateDirectionToTarget();
                PushTheTargetIfCollides();
            }
        }

        private bool WithinTheVerticalCollisionRange() => height <= colliderData.collisionHeight;
        private bool OnTheDamageHeight() => height <= colliderData.damageHeight;

        private void UpdateDirectionToTarget(){
            Vector3 vector = (pushable.transform.position - transform.position).FlatV3();
            direction = vector.normalized;
        }

        private void PushTheTargetIfCollides()
        {
            if (WithinTheVerticalCollisionRange()){
                PushTheTargetAway();
            }
        }

        private void PushTheTargetAway(){
            Vector3 force = direction * colliderData.pushPower;
            pushable.ApplyForce(force);
        }

        private void DamageTheTarget()
        {
            if (isActive && withinDamageRange){
                Vector3 impact = (direction + new Vector3(0f, UPWARD_IMPACT, 0f)) * colliderData.impact;
                DamagePackage damagePackage = new DamagePackage(colliderData.damage, impact, transform.position);

                damageable.ReceiveDamage(damagePackage);
            }
        }

# if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (visualize)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, data.Collider.radius);
                Gizmos.color = Color.white;

                float centerY = data.Collider.collisionHeight / 2f;
                Vector3 center = transform.position + new Vector3(0f, centerY, 0f);
                float side = data.Collider.radius * 2f;
                Vector3 size = new Vector3(side, data.Collider.collisionHeight, side);

                Handles.DrawWireCube(center, size);
            }
        }
#endif
    }
}

