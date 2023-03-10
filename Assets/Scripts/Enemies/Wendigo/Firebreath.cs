using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace WendigoCharacter {
    public class Firebreath : MonoBehaviour
    {
        [Header("Required Components")]
        [SerializeField] private FirebreathCollider firebreathCollider;
        [SerializeField] private ParticleSystem firebreathVFX;
        [SerializeField] private GameObject flameVFX;
        [SerializeField] private Chronos chronos;

        [Header("Settings")]
        [SerializeField] private bool visualizeRaycast;
        [SerializeField] private float deviation = 0.1f;
        [SerializeField] private float flameVFXTime = 0.3f;

        private IObjectPooler pooler;
        private List<Vector3> testHitPoints = new List<Vector3>();

        private bool isSpewingFire;

        private void Start()
        {
            pooler = PoolHolder.Instance;
        }

        public void Launch()
        {
            isSpewingFire = true;
            firebreathVFX.Play();
        }

        public void Stop()
        {
            isSpewingFire = false;
            firebreathVFX.Stop();
        }

        public void UpdateFire()
        {
            if (isSpewingFire)
                firebreathCollider.ActUponColliders(SetOnFire);
        }

        private void SetOnFire(Collider subject)
        {
            IInflammable inflammable = subject.GetComponent<IInflammable>();

            if (inflammable == null)
                Debug.Log(subject.transform.parent.name);

            inflammable.SetOnFire();
        }
    }
}

