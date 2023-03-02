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
    }

    private void DamageACharacter(Collider hitCollider, Vector3 hitPos)
    {
        DamagePackage damagePackage = GetDamagePackage(hitPos);
        hitCollider.gameObject.SendMessage("ReceiveDamage", damagePackage, SendMessageOptions.DontRequireReceiver);
    }

    private void PullDownAProp(Collider hitCollider, Vector3 hitPos)
    {
        Vector3 collapseDirection = (hitPos.FlatV3() - data.Position.FlatV3()).normalized;
        hitCollider.gameObject.SendMessage("PullDown", collapseDirection, SendMessageOptions.DontRequireReceiver);
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
