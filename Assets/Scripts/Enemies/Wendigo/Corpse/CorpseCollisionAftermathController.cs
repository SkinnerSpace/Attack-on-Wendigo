using UnityEngine;

namespace WendigoCharacter
{
    public class CorpseCollisionAftermathController
    {
        private WendigoData data;
        private Rigidbody body;
        private ShakeSettings shake;

        private float collisionPower;

        public CorpseCollisionAftermathController(WendigoData data, Rigidbody body, ShakeSettings shake){
            this.data = data;
            this.body = body;
            this.shake = shake;
        }

        public void ActUponCollisionPoint(RaycastHit hit)
        {
            CalculateCollisionPower();
            HitTheSurface(hit);
            ShakeTheEarth(hit);
        }

        private void CalculateCollisionPower()
        {
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
    }
}


