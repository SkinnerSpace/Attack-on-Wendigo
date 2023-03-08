using UnityEngine;
using UnityEditor;

namespace WendigoCharacter
{
    public class FirebreathCollider : MonoBehaviour
    {
        [SerializeField] private float radiusOuter = 10f;
        [SerializeField] private float radiusInner = 1f;
        [Range(0, 360)]
        [SerializeField] private float fovDeg = 45;
        [SerializeField] private float distanceOffset;
        [SerializeField] private int coneDetail = 10;
        [SerializeField] private Transform coneTarget;

        private float FovRad => fovDeg * Mathf.Deg2Rad;

        private void GworCone()
        {
            // Make a lerp between 0 and 1
            // So that the cone grows and detects objects gradually
            // Then it disappears from the center
            // DO IT!
        }

        private void FindObject()
        {
            // Do sphere cast on the inflammable layer
            // Put inflammables into the list
            // Go through the list and check if an object inside the cone
            // If so then burn it down
        }

        public bool ConeContains(Vector3 position)
        {
            if (SphereContains(position) == false)
                return false;

            Vector3 ownPosition = transform.position + (transform.forward * distanceOffset);
            Vector3 dirToTarget = (position - ownPosition).normalized;
            float angleRad = AngleBetweenNormalizedVectors(transform.forward, dirToTarget);
            bool fitsTheAngle = angleRad < FovRad / 2;

            return fitsTheAngle; 
        }

        private bool SphereContains(Vector3 position)
        {
            Vector3 ownPosition = transform.position + (transform.forward * distanceOffset);
            float distance = Vector3.Distance(ownPosition, position);

            Gizmos.DrawWireSphere(ownPosition, radiusOuter);
            Gizmos.DrawWireSphere(ownPosition, radiusInner);

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

        private void OnDrawGizmos() => DrawCone();

        public void DrawCone()
        {
            ReactOnCollision();
            SetUpMatrix();
            DrawRings();
        }

        private void ReactOnCollision()
        {
            if (coneTarget != null)
                Gizmos.color = Handles.color = ConeContains(coneTarget.position) ? Color.white : Color.red;
        }

        private void SetUpMatrix()
        {
            Matrix4x4 prevMtx = Gizmos.matrix;
            SetGizmoMatrix(Gizmos.matrix * Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one));
            SetGizmoMatrix(prevMtx);
        }

        private void SetGizmoMatrix(Matrix4x4 m) => Gizmos.matrix = Handles.matrix = m;

        private void DrawRings()
        {
            for (int i = 0; i < coneDetail + 1; i++)
                DrawRing(i);
        }

        private void DrawRing(int i)
        {
            float percent = i / (float)coneDetail;
            float rad = Mathf.Lerp(radiusInner, radiusOuter, percent);
            DrawRingWithRadius(rad);
        }

        private void DrawRingWithRadius(float inRadius)
        {
            float halfFovRad = FovRad / 2;

            float dist = GetDistance(inRadius, halfFovRad);
            float radius = inRadius * Mathf.Sin(halfFovRad);

            Vector3 center = transform.position + (transform.forward * dist) + (transform.forward * distanceOffset);
            Handles.DrawWireDisc(center, transform.forward, radius);
        }

        private float GetDistance(float radius, float halfFovRad) => radius * Mathf.Cos(halfFovRad);
    }
}
