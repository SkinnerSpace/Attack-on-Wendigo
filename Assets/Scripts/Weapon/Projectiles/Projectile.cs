using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    private const float GRAVITY = 9.8f;

    [SerializeField] private Rigidbody body;

    private Vector3 velocity;

    private bool launched;

    private void Update()
    {
        /*
        if (launched)
        {
            velocity -= new Vector3(0f, GRAVITY, 0f) * Time.deltaTime; 
            transform.position += velocity * Time.deltaTime;
        }
        */
    }

    public void Launch(Vector3 force)
    {
        //velocity = transform.forward * impulse;
        body.AddForce(force, ForceMode.Impulse);
        launched = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("COLLISION " + collision.collider);
    }

    
}
