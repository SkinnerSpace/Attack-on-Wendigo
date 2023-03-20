using UnityEngine;
using UnityEditor;

namespace WendigoCharacter
{
    public class CorpseSphereCollisionShape : CorpseCollisionShape
    {
        [SerializeField] private float radius;
        public override Vector3 Center => transform.position + offset;

        protected override int CheckCollision() => Physics.OverlapSphereNonAlloc(Center, radius, colliders, mask);

        public override void Visualize()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Center, radius);
            Gizmos.color = Color.white;
        }
    }
}
