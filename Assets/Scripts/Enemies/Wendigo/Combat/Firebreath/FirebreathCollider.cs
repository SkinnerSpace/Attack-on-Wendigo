using System;
using UnityEngine;

namespace WendigoCharacter
{
    public class FirebreathCollider : MonoBehaviour
    {
        [SerializeField] private WendigoData data;
        [SerializeField] private Chronos chronos;

        private FirebreathColliderData colliderData => data.Firebreath.Collider;

        private Collider[] hitColliders;

        private void Awake()
        {
            hitColliders = new Collider[colliderData.CollidersLimit];
        }

        public void Expand()
        {
            if (colliderData.CurrentExpansionTime < colliderData.ExpansionTime)
            {
                colliderData.CurrentExpansionTime += chronos.DeltaTime;
                colliderData.ObservableExpansion = (colliderData.CurrentExpansionTime / colliderData.ExpansionTime).Clamp01();
            }
        }

        public void Shrink()
        {
            colliderData.CurrentExpansionTime = 0f;
            colliderData.ObservableExpansion = 0f;
        }

        public void ActUponColliders(Action<Collider> actUpon)
        {
            UpdateDerivatives();

            Vector3 center = colliderData.ObservableCenter;
            float radius = colliderData.ObservableRadius * colliderData.ObservableExpansion;

            int collidersCount = Physics.OverlapSphereNonAlloc(center, radius, hitColliders, ComplexLayers.Inflammable);

            for (int i = 0; i < collidersCount; i++)
                ActUponCollidersInTheArea(hitColliders[i], actUpon);
        }

        public void UpdateDerivatives()
        {
            colliderData.ObservableCenter = transform.position + (transform.forward * colliderData.ObservableRadius * colliderData.ObservableExpansion);
            colliderData.CollisionCenter = transform.position + (transform.forward * colliderData.DistanceOffset);
            colliderData.FOVRad = colliderData.FOVDeg * Mathf.Deg2Rad;
        }

        private void ActUponCollidersInTheArea(Collider hitCollider, Action<Collider> actUpon)
        {
            Vector3 closestPoint = hitCollider.ClosestPoint(transform.position);
            Vector3 centralPoint = hitCollider.bounds.center;

            if (ConeContains(centralPoint) || ConeContains(closestPoint))
                actUpon(hitCollider);
        }

        private bool ConeContains(Vector3 position)
        {
            if (SphereContains(position) == false)
                return false;

            Vector3 dirToTarget = (position - colliderData.CollisionCenter).normalized;
            float angleRad = AngleBetweenNormalizedVectors(transform.forward, dirToTarget);
            bool fitsTheAngle = angleRad < colliderData.FOVRad / 2;

            return fitsTheAngle; 
        }

        private bool SphereContains(Vector3 position)
        {
            float distance = Vector3.Distance(colliderData.ObservableCenter, position);
            bool inTheArea = distance <= colliderData.ObservableRadius;

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
