using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WendigoCharacter
{
    public class AnimationEventController : MonoBehaviour
    {
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
            ShakeTheEarth();
            sFXPlayer.PlayStompSFX(leftStompPoint.position);
            pooler.SpawnFromThePool("StompSnow", leftStompPoint.position, Quaternion.identity);
        }

        public void RightStomp()
        {
            rightStompBox.Activate();
            ShakeTheEarth();
            sFXPlayer.PlayStompSFX(rightStompPoint.position);
            pooler.SpawnFromThePool("StompSnow", rightStompPoint.position, Quaternion.identity);
        }

        private void ShakeTheEarth()
        {
            /*if (CharacterData.Instance.IsGrounded)
            {
                float dist = Vector3.Distance(transform.position, CharacterData.Instance.Position);
                ScreenShake.Create().withTime(0.3f).WithAxis(1f, 1f, 0f).WithStrength(0.25f, 2f).WithCurve(4f, 0.1f, 0.25f).WithAttenuation(dist, 200f).Launch();
            }*/
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
