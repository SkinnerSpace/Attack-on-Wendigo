using System;
using UnityEngine;

namespace WendigoCharacter
{
    public class FirebreathCollider : MonoBehaviour
    {
        [SerializeField] private WendigoData data;
        private FirebreathColliderData colliderData => data.Firebreath.Collider;

        private Collider[] hitColliders;

        private void Awake()
        {
            hitColliders = new Collider[colliderData.CollidersLimit];
        }

        public void ActUponColliders(Action<Collider> actUpon)
        {
            UpdateDerivatives();
            int collidersCount = Physics.OverlapSphereNonAlloc(colliderData.ObservableCenter, colliderData.ObservableRadius, hitColliders, ComplexLayers.Inflammable);

            for (int i = 0; i < collidersCount; i++)
                ActUponCollidersInTheArea(hitColliders[i], actUpon);

            CastSurfaceFlameRay();
        }

        public void UpdateDerivatives()
        {
            colliderData.ObservableCenter = transform.position + (transform.forward * colliderData.ObservableDistanceOffset);
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

        private void CastSurfaceFlameRay()
        {
            
        }

        private void OnDrawGizmos()
        {
            //Gizmos.DrawRay(transform.position, transform.forward * data.Firebreath.Collider.)
        }
    }
}
