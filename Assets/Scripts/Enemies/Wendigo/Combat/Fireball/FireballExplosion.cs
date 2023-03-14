using UnityEngine;

public class FireballExplosion
{
    private const int MAX_COLLIDERS = 20;
    private IFireballData data;

    public FireballExplosion(IFireballData data)
    {
        this.data = data;
    }

    public void Explode()
    {
        PoolHolder.Instance.SpawnFromThePool("FireballExplosion", data.Position, Quaternion.identity);

        Collider[] hitColliders = new Collider[MAX_COLLIDERS];
        int numColliders = Physics.OverlapSphereNonAlloc(data.Position, data.ExplosionRadius, hitColliders, ComplexLayers.Exploding);

        for (int i = 0; i < numColliders; i++)
            Damage(hitColliders[i]); 
    }

    private void Damage(Collider hitCollider)
    {
        Vector3 hitPos = hitCollider.transform.position;

        DamageACharacter(hitCollider, hitPos);
        PullDownAProp(hitCollider, hitPos);
        SetOnFire(hitCollider);
    }

    private void DamageACharacter(Collider hitCollider, Vector3 hitPos)
    {
        IHitBox character = hitCollider.GetComponent<IHitBox>();

        if (character != null){
            DamagePackage damagePackage = GetDamagePackage(hitPos);
            character.ReceiveDamage(damagePackage);
        }
    }

    private void PullDownAProp(Collider hitCollider, Vector3 hitPos)
    {
        ICollapsible prop = hitCollider.GetComponent<ICollapsible>();

        if (prop != null) {
            Vector3 collapseDirection = (hitPos.FlatV3() - data.Position.FlatV3()).normalized;
            prop.PullDown(collapseDirection);
        }
    }

    private void SetOnFire(Collider hitCollider)
    {
        IInflammable inflammable = hitCollider.GetComponent<IInflammable>();

        if (inflammable != null){
            inflammable.SetOnFire();
        }
    }

    private DamagePackage GetDamagePackage(Vector3 colliderPosition)
    {
        float power = GetPower(colliderPosition);
        int damage = GetDamage(power);
        Vector3 impact = GetImpact(colliderPosition, power);

        DamagePackage damagePackage = new DamagePackage(damage, impact, data.Position);
        return damagePackage;
    }

    private float GetPower(Vector3 colliderPosition)
    {
        float distance = Vector3.Distance(data.Position, colliderPosition) - data.CollisionRadius;
        float distancePercent = (1f - (distance / data.ExplosionRadius)).Clamp01();
        float power = Easing.QuadEaseIn(distancePercent);

        return power;
    }

    private int GetDamage(float power)
    {
        int adjustedDamage = Mathf.RoundToInt(data.Damage * power);
        adjustedDamage = Mathf.Max(1, adjustedDamage);

        return adjustedDamage;
    }

    private Vector3 GetImpact(Vector3 colliderPosition, float power)
    {
        float forece = data.Impact * power;
        Vector3 impact = (colliderPosition - data.Position).normalized * forece;

        return impact;
    }
}
