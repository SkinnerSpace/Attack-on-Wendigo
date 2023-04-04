using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class LimbParticles
{
    [SerializeField] private List<ParticleSystem> flesh;
    [SerializeField] private List<ParticleSystem> bones;
    [SerializeField] private List<ParticleSystem> hair;

    public void PlayFleshExplosion() => PlayParticles(flesh);

    public void PlayBonesExplosion() => PlayParticles(bones);

    public void PlayHairCut() => PlayParticles(hair);

    private void PlayParticles(List<ParticleSystem> particles){
        foreach (ParticleSystem particle in particles)
            particle.Play();
    }
}