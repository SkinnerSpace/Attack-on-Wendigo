using System.Collections.Generic;
using UnityEngine;

public class SurfaceHitBuilder : ISurfaceHitBuilder
{
    private ParticleSystem particle;
    private AudioPlayer audioPlayer;
    private ParticleCountManager countManager;

    private float tangent;
    private float sfxVolume;

    public SurfaceHitBuilder(ParticleSystem particle, AudioPlayer audioPlayer)
    {
        this.particle = particle;
        this.audioPlayer = audioPlayer;
        countManager = new ParticleCountManager();
    }

    public ISurfaceHitBuilder WithPosition(Vector3 position)
    {
        particle.transform.position = position;
        audioPlayer.WithPosition(position);

        return this;
    }

    public ISurfaceHitBuilder WithAngle(Vector3 direction, Vector3 normal)
    {
        particle.transform.rotation = SurfaceHitMirror.ReflectRotation(direction, normal);
        tangent = Mathf.Abs(Vector3.Dot(direction, normal));

        return this;
    }

    public ISurfaceHitBuilder WithShape(float radius, float angle)
    {
        ParticleSystem.ShapeModule shape = particle.shape;

        shape.radius = radius;
        shape.angle = angle;

        sfxVolume = Easing.QuadEaseOut((radius / 1f).Clamp01());
        return this;
    }

    public ISurfaceHitBuilder WithScale(float scale)
    {
        particle.transform.localScale = new Vector3(scale, scale, scale);
        return this;
    }

    public ISurfaceHitBuilder WithCount(short minCount, short maxCount)
    {
        countManager.SetCount(minCount, maxCount);

        return this;
    }

    public void Launch()
    {
        audioPlayer.WithVolume(sfxVolume);

/*        countManager.AdjustCount(tangent);
        countManager.ApplyCount(particle);*/

        particle.Play();
        audioPlayer.PlayOneShot();
    }
}
