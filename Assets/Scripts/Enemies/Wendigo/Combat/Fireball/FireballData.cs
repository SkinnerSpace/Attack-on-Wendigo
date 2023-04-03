using UnityEngine;

public class FireballData : MonoBehaviour, IFireballData
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float impact = 500f;
    [SerializeField] private bool isActive;

    [Header("Radius")]
    [SerializeField] private float collisionRadius = 1.2f;
    [SerializeField] private float explosionRadius = 15f;
    [Range(1,3)]
    [SerializeField] private int raycastDetail = 1;

    [Header("Boundaries")]
    [SerializeField] private float maxHorizontalBoundaries;
    [SerializeField] private float maxVerticalBoundary;

    public float Speed => speed;
    public float CollisionRadius => collisionRadius;
    public float ExplosionRadius => explosionRadius;
    public int Detail => raycastDetail;

    public int Damage => damage;
    public float Impact => impact;

    public float MaxHorizontalBoundary => maxHorizontalBoundaries;
    public float MaxVerticalBoundary => maxVerticalBoundary;

    public Vector3 Position { get { return transform.position; } set { transform.position = value; } }
    public Vector3 Forward => transform.forward;

    public bool IsActive { get { return isActive; } set { isActive = value; } }

    public Transform owner;
}
