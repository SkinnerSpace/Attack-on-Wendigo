using UnityEngine;

public class GoreSFXPlayer : LimbSFXPlayer
{
    public GoreSFXPlayer(Transform transform, GoreSFXData data){
        this.transform = transform;
        hitPlayer = AudioPlayer.Create(data.fleshHitSFX).WithVariety(3).WithPitch(-2f, 2f);
        smashPlayer = AudioPlayer.Create(data.fleshSmashSFX).WithVariety(7).WithPitch(-2f, 2f);
    }
}
