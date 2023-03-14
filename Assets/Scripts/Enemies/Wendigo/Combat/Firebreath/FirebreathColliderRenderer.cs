using UnityEngine;
using UnityEditor;

namespace WendigoCharacter
{
    public class FirebreathColliderRenderer : MonoBehaviour
    {
        [SerializeField] private FirebreathCollider firebreathCollider;
        [SerializeField] private WendigoData data;

        private FirebreathColliderData colliderData => data.Firebreath.Collider;
        private FirebreathColliderRendererData rendererData => data.Firebreath.ColliderRenderer;
        private bool atLeastOneCollider;

        private void OnDrawGizmos()
        {
            if (rendererData.IsActive && firebreathCollider != null)
                Visualize();
        }

        public void Visualize()
        {
            firebreathCollider.ActUponColliders(ShowColliders);
            SetUpMatrix();
            DrawObservableArea();
            DrawRings();
        }

        private void ShowColliders(Collider subject)
        {
            atLeastOneCollider = true;

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(subject.transform.position, 1f);
            Gizmos.color = Color.white;
        }

        private void SetUpMatrix()
        {
            Matrix4x4 prevMtx = Gizmos.matrix;
            SetGizmoMatrix(Gizmos.matrix * Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one));
            SetGizmoMatrix(prevMtx);
        }

        private void SetGizmoMatrix(Matrix4x4 m) => Gizmos.matrix = Handles.matrix = m;

        private void DrawObservableArea()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(colliderData.ObservableCenter, colliderData.ObservableRadius * colliderData.ObservableExpansion);
        }

        private void DrawRings()
        {
            for (int i = 0; i < rendererData.Detail + 1; i++)
                DrawRingAlongTheLine(i);

            atLeastOneCollider = false;
        }

        private void DrawRingAlongTheLine(int i)
        {
            float percent = i / (float)rendererData.Detail;
            float rad = Mathf.Lerp(colliderData.RadiusInner, colliderData.RadiusOuter, percent);
            DrawRingWithRadius(rad);
        }

        private void DrawRingWithRadius(float inRadius)
        {
            float halfFovRad = colliderData.FOVRad / 2;

            float dist = GetDistance(inRadius, halfFovRad);
            float radius = inRadius * Mathf.Sin(halfFovRad);
            Vector3 ringCenter = transform.position + (transform.forward * dist) + (transform.forward * colliderData.DistanceOffset);

            DrawRing(ringCenter, radius);
        }

        private void DrawRing(Vector3 ringCenter, float radius)
        {
            Handles.color = atLeastOneCollider ? Color.red : Color.white;
            Handles.DrawWireDisc(ringCenter, transform.forward, radius);
            Handles.color = Color.white;
        }

        private float GetDistance(float radius, float halfFovRad) => radius * Mathf.Cos(halfFovRad);
    }
}
