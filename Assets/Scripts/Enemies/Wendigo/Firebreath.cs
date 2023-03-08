using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace WendigoCharacter {
    public class Firebreath : MonoBehaviour
    {
        [Header("Required Components")]
        [SerializeField] private ParticleSystem firebreathVFX;
        [SerializeField] private GameObject flameVFX;
        [SerializeField] private Chronos chronos;

        [Header("Settings")]
        [SerializeField] private bool visualizeRaycast;
        [SerializeField] private float deviation = 0.1f;
        [SerializeField] private float flameVFXTime = 0.3f;

        private WendigoData data;
        private IObjectPooler pooler;

        private List<Vector3> testHitPoints = new List<Vector3>();

        private bool isSpewingFire;

        private void Start()
        {
            pooler = PoolHolder.Instance;
        }

        public void Initialize(WendigoData data) => this.data = data;

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
                Cast();
        }

        private void Cast()
        {
            testHitPoints.Clear();

            for (int x = -data.FirePrecision; x < data.FirePrecision + 1; x++)
            {
                for (int y = -data.FirePrecision; y < data.FirePrecision + 1; y++)
                {
                    ExecuteDetection(x, y);
                }
            }
        }

        private void ExecuteDetection(int x, int y)
        {
            IntVector2 vector = new IntVector2(x, y);

            Detect(vector, ComplexLayers.Inflammable, SetOnFire);

            if (visualizeRaycast)
                Detect(vector, ComplexLayers.Exploding, TestRaycast);
        }

        private void Detect(IntVector2 vector, LayerMask mask, Action<RaycastHit> onDetected)
        {
            Vector3 direction = GetDirection(vector);

            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, data.FireRange, mask))
                onDetected(hit);
        }

        private Vector3 GetDirection(IntVector2 vector)
        {
            float xOffset = GetOffset(vector.x);
            float yOffset = GetOffset(vector.y);
            Vector3 dir = new Vector3(xOffset, yOffset, 0f) + transform.forward;
            dir = dir.normalized;

            return dir;
        }

        private float GetOffset(int value) => (value / (float)data.FirePrecision) * data.FireScatter;

        private void SetOnFire(RaycastHit hit)
        {
            IInflammable inflammable = hit.transform.GetComponent<IInflammable>();
            inflammable.SetOnFire();
        }

        private void TestRaycast(RaycastHit hit) => testHitPoints.Add(hit.point);

        private void OnDrawGizmos()
        {
            if (visualizeRaycast)
            {
                Gizmos.color = Color.red;

                foreach (Vector3 point in testHitPoints)
                    Gizmos.DrawLine(transform.position, point);
            }
        }
    }
}

