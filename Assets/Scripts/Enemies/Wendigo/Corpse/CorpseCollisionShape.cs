using UnityEngine;
using System;

namespace WendigoCharacter
{
    public abstract class CorpseCollisionShape : MonoBehaviour
    {
        protected Collider[] colliders;
        protected LayerMask mask => ComplexLayers.Landscape;

        private int collidersCount;
        private bool isCollided;

        public Vector3 Center => transform.position;

        private event Action onCollisionEnter;

        public virtual void UpdateCollision()
        {
            ClearColliders();
            collidersCount = CheckCollision();
            NotifyOnCollisionUpdate();
        }

        public virtual void Subscribe(Action onCollisionEnter){
            this.onCollisionEnter += onCollisionEnter;
        }

        private void ClearColliders() => colliders = new Collider[1];

        protected abstract int CheckCollision();

        private void NotifyOnCollisionUpdate()
        {
            if (CollisionIsEntered()){
                isCollided = true;
                onCollisionEnter?.Invoke();
            }
            else if (CollisionIsExited()){
                isCollided = false;
            }
        }

        private bool CollisionIsEntered() => collidersCount > 0 && !isCollided;
        private bool CollisionIsExited() => collidersCount == 0 && isCollided;

#if UNITY_EDITOR
        public abstract void Visualize();
#endif
    }
}
