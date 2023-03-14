using System;
using System.Collections.Generic;
using UnityEngine;

namespace WendigoCharacter
{
    public class RangeCombatManager : WendigoPlugableComponent
    {
        private WendigoData data;

        public override void Initialize(IWendigo wendigo) => data = wendigo.Data;

        public void PrepareToAttack()
        {
            float distance = TargetExist() ? GetDistance() : 0f;
            PrepareToBreathFire(distance);
            PrepareToCastAFireball(distance);
        }

        private bool TargetExist() => (data.Target != null);
        private float GetDistance() => (data.Target.position.FlatV3() - data.Transform.Position.FlatV3()).magnitude;

        private void PrepareToBreathFire(float distance){
            data.Firebreath.IsReadyToUse = IsAbleToUseAFirebreath() ? OnTheFirebreathDistance(distance) : false;
        }

        private bool IsAbleToUseAFirebreath()
        {
            return data.Target != null && 
                   data.Firebreath.OnTarget && 
                   data.Firebreath.IsCharged;
        }

        private bool OnTheFirebreathDistance(float distance)
        {
            return distance >= data.Firebreath.MinDistance && 
                   distance <= data.Firebreath.MaxDistance;
        }

        private void PrepareToCastAFireball(float distance){
            data.Fireball.IsReadyToUse = IsAbleToUseAFireball() ? OnTheFireballDistance(distance) : false;
        }

        private bool IsAbleToUseAFireball()
        {
            return data.Target != null &&
                   data.Fireball.OnTarget &&
                   data.Fireball.IsCharged;
        }

        private bool OnTheFireballDistance(float distance)
        {
            return distance >= data.Fireball.MinDistance && 
                   distance <= data.Fireball.MaxDistance;
        }
    }
}
