using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class LimbParticles
{
    [SerializeField] private List<ParticleSystem> flesh;
    [SerializeField] private List<ParticleSystem> bones;
    [SerializeField] private List<ParticleSystem> hair;

    [SerializeField] private List<ParticleSoundSystem> fleshSound;
    [SerializeField] private List<ParticleSoundSystem> boneSound;

    public void PlayFleshExplosion()
    {
        PlayParticles(flesh);
        PlaySounds(fleshSound);
    }

    public void PlayBonesExplosion()
    {
        PlayParticles(bones);
        PlaySounds(boneSound);
    }

    public void PlayHairCut() => PlayParticles(hair);

    private void PlayParticles(List<ParticleSystem> particles){
        foreach (ParticleSystem particle in particles)
            particle.Play();
    }

    private void PlaySounds(List<ParticleSoundSystem> sounds)
    {
        foreach (ParticleSoundSystem sound in sounds)
            sound.SwitchOn();
    }
}
