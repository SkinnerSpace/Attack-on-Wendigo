using UnityEngine;

namespace WendigoCharacter
{
    public class MockWendigo
    {
        public WendigoData Data { get; set; }
        public IChronos Chronos { get; set; }
        public Animator Animator { get; }
        public IHitBox[] HitBoxes { get; set; }

        public MockWendigo(WendigoData data) => Data = data;

        public MockWendigo WithHitBoxes(int amount)
        {
            HitBoxes = new DetachedHitBox[amount];

            for (int i = 0; i < amount; i++)
                HitBoxes[i] = new DetachedHitBox();

            return this;
        }

        public void SetTarget(Transform target)
        {

        }
    }
}