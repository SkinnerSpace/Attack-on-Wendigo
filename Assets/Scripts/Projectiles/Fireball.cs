using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed = 100f;
    [SerializeField] private float collisionRadius = 0.5f;
    [SerializeField] private float explosionRadius = 5f;

    [Header("Damage")]
    [SerializeField] private int damage;
    [SerializeField] private float impact;

    private int maxColliders = 20;
    private bool isActive = true;
    private bool isExploded;

    private void Update()
    {
        if (isActive)
            CheckCollision();
    }

    private void FixedUpdate()
    {
        if (isActive)
            Move();
    }

    private void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void CheckCollision()
    {
        Collider[] hitColliders = new Collider[1];
        Physics.OverlapSphereNonAlloc(transform.position, collisionRadius, hitColliders, ComplexLayers.Exploding);
        Collide(hitColliders[0]);
    }

    private void Collide(Collider hitCollider)
    {
        if (hitCollider != null)
        {
            Explode();
        }
    }

    private void Explode()
    {
        isActive = false;

        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, hitColliders, ComplexLayers.Exploding);

        for (int i = 0; i < numColliders; i++)
            Damage(hitColliders[i]);

        isActive = false;
        isExploded = true;
        Destroy(gameObject);
    }

    private void Damage(Collider hitCollider)
    {
        float distance = Vector3.Distance(transform.position, hitCollider.transform.position) - collisionRadius;
        float distancePercent = (1f - (distance / explosionRadius)).Clamp01();
        float power = Easing.QuadEaseIn(distancePercent);

        int adjustedDamage = Mathf.RoundToInt(damage * power);
        float adjustedImpact = impact * power;
        Vector3 impactForce = (hitCollider.transform.position - transform.position).normalized * adjustedImpact;

        //Debug.Log(hitCollider.transform.name + " Dist " + distance + " Power " + power + " Damage " + adjustedDamage + " Impact " + adjustedImpact);

        DamagePackage damagePackage = new DamagePackage(adjustedDamage, impactForce, transform.position);
        hitCollider.gameObject.SendMessage("ReceiveDamage", damagePackage, SendMessageOptions.DontRequireReceiver);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, radius);

        if (isExploded)
        {
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}