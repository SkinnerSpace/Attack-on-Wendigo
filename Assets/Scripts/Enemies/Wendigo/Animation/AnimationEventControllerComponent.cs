using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WendigoCharacter
{
    public class AnimationEventControllerComponent : MonoBehaviour
    {
        private const float SHAKE_MAX_DISTANCE = 200f;

        [Header("Required Components")]
        [SerializeField] private WendigoData data;
        [SerializeField] private WendigoSFXPlayer sFXPlayer;
        [SerializeField] private FireballSpawnerComponent fireballSpawner;
        [SerializeField] private Firebreath firebreath;

        [Header("Stomp")]
        [SerializeField] private StompDamageBox leftStompBox;
        [SerializeField] private StompDamageBox rightStompBox;
        [SerializeField] private Transform leftStompPoint;
        [SerializeField] private Transform rightStompPoint;

        private IObjectPooler pooler;

        private void Start()
        {
            pooler = PoolHolder.Instance;
        }

        public void LeftStomp()
        {
            leftStompBox.Activate();
            ShakeTheEarth(leftStompPoint.position);
            sFXPlayer.PlayStompSFX(leftStompPoint.position);
            pooler.SpawnFromThePool("StompSnow", leftStompPoint.position, Quaternion.identity);
        }

        public void RightStomp()
        {
            rightStompBox.Activate();
            ShakeTheEarth(rightStompPoint.position);
            sFXPlayer.PlayStompSFX(rightStompPoint.position);
            pooler.SpawnFromThePool("StompSnow", rightStompPoint.position, Quaternion.identity);
        }

        public void ShakeTheEarth(Vector3 sourcePos)
        {
            if (data.Target.IsGrounded)
            {
                ShakeBuilder.Create().
                    withTime(0.3f).
                    WithAxis(1f, 1f, 0f).
                    WithStrength(0.12f, 1.7f).
                    WithCurve(4f, 0.1f, 0.25f).
                    WithAttenuation(sourcePos, data.Target.transform, SHAKE_MAX_DISTANCE).
                    BuildAndLaunch(ShakeManagerComponent.Instance);
            }
        }

        public void StartCastingFireball()
        {
            fireballSpawner.PlayCastVFX();
            sFXPlayer.PlayFireballCastSFX(fireballSpawner.transform.position);
        }

        public void SpawnFireball() => fireballSpawner.SpawnFireball();
        public void FireballCastIsOver() => data.Fireball.IsOver = true;

        public void RoarOnArrival()
        {
            sFXPlayer.PlayArrivalRoarSFX();
            sFXPlayer.PlayArrivalBoneCrackSFX();
        }

        public void Inhale(){
            sFXPlayer.PlayInhaleSFX();
        }

        public void LaunchFirebreath()
        {
            firebreath.Launch();
            sFXPlayer.PlayExhaleSFX();
        }

        public void StopFirebreath()
        {
            firebreath.Stop();
            data.Firebreath.IsOver = true;
        }
    }
}
