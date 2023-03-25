using UnityEngine;

public class GoreSFXPlayer
{
    private Transform transform; 
    private AudioPlayer hitPlayer;
    private AudioPlayer fleshSmashPlayer;
    
    public GoreSFXPlayer(Transform transform, GoreSFXData data)
    {
        this.transform = transform;
        hitPlayer = AudioPlayer.Create(data.hitSFX).WithVariety(3).WithPitch(-2f, 2f);
        fleshSmashPlayer = AudioPlayer.Create(data.fleshSmashSFX).WithVariety(7).WithPitch(-2f, 2f);
    }

    public void PlayHitSFX(){
        hitPlayer.WithPosition(transform.position).
                  PlayOneShot();
    }

    public void PlaySmashSFX(){
        fleshSmashPlayer.WithPosition(transform.position).
                         PlayOneShot();

    }
}
