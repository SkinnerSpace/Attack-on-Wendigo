using UnityEngine;

public abstract class LimbSFXPlayer
{
    protected Transform transform;
    protected AudioPlayer hitPlayer;
    protected AudioPlayer smashPlayer;

    public void PlayHitSFX(){
        hitPlayer.WithPosition(transform.position).
                  PlayOneShot();
    }

    public void PlaySmashSFX(){
        smashPlayer.WithPosition(transform.position).
                         PlayOneShot();

    }
}