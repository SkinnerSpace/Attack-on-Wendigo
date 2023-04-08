using UnityEngine;
using System.Collections.Generic;

public class VoicedParticleSystem : MonoBehaviour
{
    [SerializeField] private List<ParticleSoundSystem> sounds;
    private ParticleSystem particles;

    public void Play()
    {
        foreach (ParticleSoundSystem soundSystem in sounds){
            soundSystem.SwitchOn();
        }

        particles.Play();
    }
}