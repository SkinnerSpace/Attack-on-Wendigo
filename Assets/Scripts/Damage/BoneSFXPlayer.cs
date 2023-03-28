using UnityEngine;

public class BoneSFXPlayer : LimbSFXPlayer
{
    public BoneSFXPlayer(Transform transform, GoreSFXData data){
        this.transform = transform;
        hitPlayer = AudioPlayer.Create(data.bonesHitSFX).WithVariety(2).WithPitch(-2f, 2f);
        smashPlayer = AudioPlayer.Create(data.bonesSmashSFX).WithVariety(3).WithPitch(-2f, 2f);
    }
}
