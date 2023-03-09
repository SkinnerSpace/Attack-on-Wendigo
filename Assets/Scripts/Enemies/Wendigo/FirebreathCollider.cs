using System;
using UnityEngine;

namespace WendigoCharacter
{
    public class FirebreathCollider : MonoBehaviour
    {
        [Range(0, 360)]
        [SerializeField] private float fovDeg = 45;
        [SerializeField] private float radiusOuter = 10f;
        [SerializeField] private float radiusInner = 1f;
        [SerializeField] private float distanceOffset;
        [SerializeField] private int collidersLimit = 16;

        public float FOVRad { get; private set; }
        public float RadiusOuter => radiusOuter;
        public float RadiusInner => radiusInner;
        public float DistanceOffset => distanceOffset;

        public Vector3 Center { get; private set; }

        private void GworCone()
        {
            // Make a lerp between 0 and 1
            // So that the cone grows and detects objects gradually
            // Then it disappears from the center
            // DO IT!
        }

        private Vector3 GetCenter() => transform.position + (transform.forward * distanceOffset);
        private float GetFOVRad() => fovDeg * Mathf.Deg2Rad;

        public void ActUponColliders(Action<Collider> actUpon)
        {
            UpdateDerivatives();

            Collider[] hitColliders = new Collider[collidersLimit];
            int collidersCount = Physics.OverlapSphereNonAlloc(Center, radiusOuter, hitColliders, ComplexLayers.Inflammable);

            for (int i = 0; i < collidersCount; i++)
                ActUponCollidersInTheArea(hitColliders[i], actUpon);
        }

        public void UpdateDerivatives()
        {
            Center = GetCenter();
            FOVRad = GetFOVRad();
        }

        private void ActUponCollidersInTheArea(Collider hitCollider, Action<Collider> actUpon)
        {
            Vector3 closestPoint = hitCollider.ClosestPoint(transform.position);

            if (ConeContains(closestPoint))
                actUpon(hitCollider);
        }

        private bool ConeContains(Vector3 position)
        {
            if (SphereContains(position) == false)
                return false;

            Vector3 dirToTarget = (position - Center).normalized;
            float angleRad = AngleBetweenNormalizedVectors(transform.forward, dirToTarget);
            bool fitsTheAngle = angleRad < FOVRad / 2;

            return fitsTheAngle; 
        }

        private bool SphereContains(Vector3 position)
        {
            float distance = Vector3.Distance(Center, position);
            bool inTheArea = distance >= radiusInner && distance <= radiusOuter;

            return inTheArea;
        }

        static float AngleBetweenNormalizedVectors(Vector3 first, Vector3 second)
        {
            float dotProduct = Vector3.Dot(first, second);
            float angleRads = Mathf.Acos(dotProduct);
            angleRads = Mathf.Clamp(angleRads, -1f, 1f);

            return angleRads;
        }
    }
}
