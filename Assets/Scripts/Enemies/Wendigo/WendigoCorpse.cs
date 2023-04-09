using System.Collections.Generic;
using UnityEngine;

namespace WendigoCharacter
{
    public class WendigoCorpse : MonoBehaviour
    {
        [Header("Required Components")]
        [SerializeField] private WendigoData data;
        [SerializeField] private RagDollController ragdoll;
        [SerializeField] private CorpseCollisionController collisionController;
        [SerializeField] private WendigoPooledObject pooledObject;
        [SerializeField] private FunctionTimer timer;
        [SerializeField] private Transform body;
        [SerializeField] private Transform limbsRoot;
        [SerializeField] private BurySFXPlayer burySFXPlayer;

        [Header("Settings")]
        [SerializeField] private float timeTillTheFuneral = 10f;
        [SerializeField] private float funeralTime = 5f;
        [SerializeField] private float depth = 20f;

        [Header("Body Shake")]
        [SerializeField] private float power = 0.5f;
        [SerializeField] private float attack = 0.1f;
        [SerializeField] private float release = 0.1f;
        [SerializeField] private float frequency = 8f;

        [Header("Earth Shake")]
        [SerializeField] private float earthPower = 0.5f;
        [SerializeField] private float earthAngleMultiplier = 1f;
        [SerializeField] private float earthFrequency = 8f;
        [SerializeField] private float earthShakeDistance = 40f;

        private Limb[] limbs;
        private int initialLimbsCount;
        private int limbsCount;

        private IObjectPooler pooler;

        private IShake shake;
        private bool isBeingBuried;
        private Vector3 initialPosition;
        private Vector3 burialPosition;
        private float funeralProgress;
        private float currentTime;

        private void Awake(){
            shake = ShakeBuilder.Create().WithAxis(1f, 0f, 1f).WithStrength(power, 0f).WithCurve(frequency, attack, release).Build();
            limbs = limbsRoot.GetComponentsInChildren<Limb>();
            initialLimbsCount = limbs.Length;
            limbsCount = limbs.Length;
        }

        private void Start()
        {
            pooler = PoolHolder.Instance;

            foreach (Limb limb in limbs){
                limb.SubscribeOnAmputation(() => limbsCount -= 1);
            }
        }

        private void Update()
        {
            if (isBeingBuried){
                CountDown();
                Submerge();
            }
        }

        private void CountDown()
        {
            currentTime += OldChronos.DeltaTime;
            funeralProgress = Mathf.InverseLerp(0f, funeralTime, currentTime);

            if (funeralProgress >= 1f){
                isBeingBuried = false;
                pooledObject.BackToPool();
            }
        }

        private void Submerge()
        {
            shake.Update(funeralProgress);
            Vector3 displacement = shake.GetDisplacement().Position;

            float submergeProgress = Easing.QuadEaseIn(funeralProgress);
            Vector3 currentSubmergedPosition = Vector3.Lerp(initialPosition, burialPosition, submergeProgress);

            body.position = currentSubmergedPosition + displacement;
        }

        public void PrepareToFuneral(){
            timer.Set("Bury", timeTillTheFuneral, Bury);
        }

        private void Bury()
        {
            Debug.Log("BURY");
            isBeingBuried = true;

            CalculateBurialPosition();
            ragdoll.SwitchOff();
            collisionController.SwitchOff();
            burySFXPlayer.PlayBurySFX();

            LetItSnow();
            ShakeTheEarth();
        }

        private void CalculateBurialPosition()
        {
            initialPosition = body.position;
            burialPosition = initialPosition + new Vector3(0f, -depth, 0f);
        }

        private void LetItSnow()
        {
            foreach (Limb limb in limbs){
                Vector3 spawnPosition = limb.transform.position.FlatV3();

                pooler.SpawnFromThePool("FuneralSnowParticle", spawnPosition, Quaternion.identity);
                pooler.SpawnFromThePool("BloodPuddle", spawnPosition, Quaternion.identity);
            }
        }

        private void ShakeTheEarth()
        {
            if (data.Target.Exist && IsAllowedToShakeTheEarth())
            {
                float earthPowerMultiplier = (limbsCount / (float)initialLimbsCount);

                float strength = earthPower * earthPowerMultiplier;
                float angleMultiplier = earthAngleMultiplier * earthPowerMultiplier;

                ShakeBuilder.Create().
                    withTime(funeralTime).
                    WithAxis(1f, 1f, 0f).
                    WithStrength(strength, angleMultiplier).
                    WithCurve(earthFrequency, attack, release).
                    WithAttenuation(ragdoll.transform.position, data.Target.transform, earthShakeDistance).
                    BuildAndLaunch(ShakeManagerComponent.Instance);
            }
        }

        private bool IsAllowedToShakeTheEarth() => data != null && data.Target.IsGrounded;
    }
}

