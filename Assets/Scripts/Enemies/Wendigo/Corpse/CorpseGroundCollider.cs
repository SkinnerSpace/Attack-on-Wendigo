using UnityEngine;

namespace WendigoCharacter
{
    public class CorpseGroundCollider : MonoBehaviour
    {
        [SerializeField] private CorpseCollisionShape collisionShape;

        private Rigidbody body;
        private CorpseCollisionController controller;
        private ShakeSettings shake;
        private WendigoData data;

        private bool isEnabled;

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
            isEnabled = true;
            enabled = true;
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
            HitTheSurface(hit);
            ShakeTheEarth(hit);
        }

        private void HitTheSurface(RaycastHit hit)
        {
            ISurface surface = hit.transform.GetComponent<ISurface>();

            surface.Hit().
                WithPosition(hit.point).
                WithAngle(Vector3.down, Vector3.up).
                Launch();
        }

        private void ShakeTheEarth(RaycastHit hit)
        {
            float power = Mathf.InverseLerp(0f, shake.maxVelocity, body.velocity.magnitude);
            Debug.Log(transform.name + " " + power);

            float time = Rand.Range(shake.minTime, shake.maxTime);
            float strength = shake.strength * power;
            float angleMultiplier = shake.angleMultiplier * power;

            if (IsAllowedToShakeTheEarth()){
                ShakeBuilder.Create().
                    withTime(time).
                    WithAxis(1f, 1f, 0f).
                    WithStrength(strength, angleMultiplier).
                    WithCurve(shake.frequency, shake.attack, shake.release).
                    WithAttenuation(hit.point, data.Target.transform, shake.maxDistance).
                    BuildAndLaunch(ShakeManagerComponent.Instance);
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
