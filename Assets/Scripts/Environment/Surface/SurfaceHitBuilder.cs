using System.Collections.Generic;
using UnityEngine;

public class SurfaceHitBuilder
{
    private ParticleSystem particle;
    private SurfaceSFXManager sfxManager;
    private ParticleCountManager countManager;

    private float tangent;

    public SurfaceHitBuilder(ParticleSystem particle, SurfaceSFXManager sfxManager)
    {
        this.particle = particle;
        this.sfxManager = sfxManager;
        countManager = new ParticleCountManager();
    }

    public SurfaceHitBuilder WithPosition(Vector3 position)
    {
        particle.transform.position = position;
        sfxManager.SetPosition(position);

        return this;
    }

    public SurfaceHitBuilder WithAngle(Vector3 direction, Vector3 normal)
    {
        particle.transform.rotation = SurfaceHitMirror.ReflectRotation(direction, normal);
        tangent = Mathf.Abs(Vector3.Dot(direction, normal));

        return this;
    }

    public SurfaceHitBuilder WithShape(float radius, float angle)
    {
        ParticleSystem.ShapeModule shape = particle.shape;
        shape.radius = radius;
        shape.angle = angle;

        sfxManager.ChooseSFX(radius);

        return this;
    }

    public SurfaceHitBuilder WithCount(short minCount, short maxCount)
    {
        countManager.SetCount(minCount, maxCount);

        return this;
    }

    public void Launch()
    {
        countManager.AdjustCount(tangent);
        countManager.ApplyCount(particle);

        particle.Play();
        sfxManager.PlaySFX();
    }
}
