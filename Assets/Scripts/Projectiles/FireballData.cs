using UnityEngine;

public class FireballData : MonoBehaviour, IFireballData
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float collisionRadius = 1.2f;
    [SerializeField] private float explosionRadius = 15f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float impact = 500f;

    public float Speed => speed;
    public float CollisionRadius => collisionRadius;
    public float ExplosionRadius => explosionRadius;
    public int Damage => damage;
    public float Impact => impact;

    public Vector3 Position { get { return transform.position; } set { transform.position = value; } }
    public Vector3 Forward => transform.forward;

    
}
