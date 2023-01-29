using UnityEngine;

public class SnowSurface : MonoBehaviour, ISurface
{
    [SerializeField] private GameObject snowParticle;

    public void Hit(SurfaceHitPoint hitPoint, SurfaceHitCollider hitCollider)
    {
        ParticleSystem particle = Instantiate(snowParticle, hitPoint.position, Quaternion.identity).GetComponent<ParticleSystem>();
        SetRotation(particle, hitCollider.direction, hitPoint.normal);
        SetRadius(particle, hitCollider.radius);

        particle.Play();
    }

    private void SetRotation(ParticleSystem particle, Vector3 direction, Vector3 normal)
    {
        Vector3 reflectedDirection = Vector3.Reflect(direction, normal);
        particle.transform.rotation = Quaternion.LookRotation(reflectedDirection, Vector3.up);
    }

    private void SetRadius(ParticleSystem particle, float radius)
    {
        ParticleSystem.ShapeModule shape = particle.shape;
        shape.radius = radius;
    }
}

public class NullSurface : ISurface
{

}