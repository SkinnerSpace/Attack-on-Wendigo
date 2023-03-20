using UnityEngine;

namespace WendigoCharacter
{
    public class CorpseCapsuleCollisionShape : CorpseCollisionShape
    {
        [SerializeField] private Vector3 firstPoint;
        [SerializeField] private Vector3 secondPoint;
        [SerializeField] private float radius;

        private Vector3 FirstPoint => Center + firstPoint;
        private Vector3 SecondPoint => Center + secondPoint;

        protected override int CheckCollision() => Physics.OverlapCapsuleNonAlloc(FirstPoint, SecondPoint, radius, colliders, mask);

        public override void Visualize()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(FirstPoint, radius);
            Gizmos.DrawWireSphere(SecondPoint, radius);
            Gizmos.color = Color.white;
        }
    }
}
