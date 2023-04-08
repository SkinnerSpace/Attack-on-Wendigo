using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticleSoundSystem : MonoBehaviour, ISwitchable
{
    [SerializeField] private FMODUnity.EventReference collisionSFX;
    [SerializeField] private int variety;
    [SerializeField] private float pitchRange;

    [SerializeField] private ParticleSoundData data;

    private ParticleSystem particles;
    private IDictionary<uint, ParticleSystem.Particle> trackedParticles = new Dictionary<uint, ParticleSystem.Particle>();

    private AudioPlayer player;

    public void SwitchOn()
    {
        enabled = true;
    }

    public void SwitchOff()
    {
        enabled = false;
    }

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        if (data != null)
        {
            player = AudioPlayer.Create(data.collisionSFX).WithPitch(-data.pitchRange, data.pitchRange).WithVariety(data.variety);
        }
        else
        {
            player = AudioPlayer.Create(collisionSFX).WithPitch(-pitchRange, pitchRange).WithVariety(variety);
        }

        enabled = false;
    }

    private void Update()
    {
        var liveParticles = new ParticleSystem.Particle[particles.particleCount];
        particles.GetParticles(liveParticles);

        var particleDelta = GetParticleDelta(liveParticles);

        foreach (var particleRemoved in particleDelta.Removed)
        {
            player.WithPosition(particleRemoved.position).PlayOneShot();
        }

        if (particles.particleCount == 0)
        {
            enabled = false;
        }
    }

    private ParticleDelta GetParticleDelta(ParticleSystem.Particle[] liveParticles)
    {
        var deltaResult = new ParticleDelta();

        foreach (var activeParticle in liveParticles)
        {
            ParticleSystem.Particle foundParticle;
            if (trackedParticles.TryGetValue(activeParticle.randomSeed, out foundParticle))
            {
                trackedParticles[activeParticle.randomSeed] = activeParticle;
            }
            else
            {
                deltaResult.Added.Add(activeParticle);
                trackedParticles.Add(activeParticle.randomSeed, activeParticle);
            }
        }

        var updateParticlesAsDictionary = liveParticles.ToDictionary(x => x.randomSeed, x => x);
        var dictionaryKeysAsList = trackedParticles.Keys.ToList();

        foreach (var dictionaryKey in dictionaryKeysAsList)
        {
            if (updateParticlesAsDictionary.ContainsKey(dictionaryKey) == false)
            {
                deltaResult.Removed.Add(trackedParticles[dictionaryKey]);
                trackedParticles.Remove(dictionaryKey);
            }
        }

        return deltaResult;
    }

    private class ParticleDelta
    {
        public IList<ParticleSystem.Particle> Added { get; set; } = new List<ParticleSystem.Particle>();
        public IList<ParticleSystem.Particle> Removed { get; set; } = new List<ParticleSystem.Particle>();
    }
}
