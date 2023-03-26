using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class LimbParticles
{
    [SerializeField] private List<ParticleSystem> flesh;
    [SerializeField] private List<ParticleSystem> bones;

    public void PlayFleshExplosion() => PlayParticles(flesh);

    public void PlayBonesExplosion() => PlayParticles(bones);

    private void PlayParticles(List<ParticleSystem> particles){
        foreach (ParticleSystem particle in particles)
            particle.Play();
    }
}