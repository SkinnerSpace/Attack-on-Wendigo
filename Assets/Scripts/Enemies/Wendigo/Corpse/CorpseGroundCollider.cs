using UnityEngine;

namespace WendigoCharacter
{
    public class CorpseGroundCollider : MonoBehaviour, IAmputationObserver
    {
        [SerializeField] private CorpseCollisionShape collisionShape;

        private Rigidbody body;
        private CorpseCollisionController controller;
        private ShakeSettings shake;
        private WendigoData data;

        private float collisionPower;
        private bool isEnabled;
        private bool isDestroyed;

        private void Awake()
        {
            enabled = false;
            collisionShape.Subscribe(HandleCollision);

            body = transform.parent.GetComponent<Rigidbody>();
        }

        public void Initialize(CorpseCollisionController controller, WendigoData data, ShakeSettings shake){
            this.controller = controller;
            this.data = data;
            this.shake = shake;
        }

        public void SwitchOn()
        {
            if (!isDestroyed){
                isEnabled = true;
                enabled = true;
            }
        }

        public void OnAmputation()
        {
            isDestroyed = true;
            isEnabled = false;
            enabled = false;
        }

        private void Update()
        {
            if (isEnabled){
                collisionShape.UpdateCollision();
            }
        }

        private void HandleCollision()
        {
            Ray ray = new Ray(collisionShape.Center, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ComplexLayers.Landscape)){
                ActUponCollisionPoint(hit);
            }
        }

        private void ActUponCollisionPoint(RaycastHit hit)
        {
            CalculateCollisionPower();
            HitTheSurface(hit);
            ShakeTheEarth(hit);
        }

        private void CalculateCollisionPower(){
            collisionPower = Mathf.InverseLerp(0f, shake.maxVelocity, body.velocity.magnitude);
            collisionPower = Easing.QuadEaseIn(collisionPower);
        }

        private void HitTheSurface(RaycastHit hit)
        {
            ISurface surface = hit.transform.GetComponent<ISurface>();

            surface.Hit("WendigoFallSnowParticle").
                WithPosition(hit.point).
                WithAngle(Vector3.down, Vector3.up).
                WithSFXVolume(collisionPower).
                Launch();
        }

        private void ShakeTheEarth(RaycastHit hit)
        {
            if (data.Target.Exist)
            {
                float time = Rand.Range(shake.minTime, shake.maxTime);
                float strength = shake.strength * collisionPower;
                float angleMultiplier = shake.angleMultiplier * collisionPower;

                if (IsAllowedToShakeTheEarth())
                {
                    ShakeBuilder.Create().
                        withTime(time).
                        WithAxis(1f, 1f, 0f).
                        WithStrength(strength, angleMultiplier).
                        WithCurve(shake.frequency, shake.attack, shake.release).
                        WithAttenuation(hit.point, data.Target.transform, shake.maxDistance).
                        BuildAndLaunch(ShakeManagerComponent.Instance);
                }
            }
        }

        private bool IsAllowedToShakeTheEarth() => data != null && data.Target.IsGrounded;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (IsAbleToVisualize()){
                collisionShape.Visualize();
            }
        }

        private bool IsAbleToVisualize() => controller != null && collisionShape != null && controller.Visualize;
#endif
    }
}
