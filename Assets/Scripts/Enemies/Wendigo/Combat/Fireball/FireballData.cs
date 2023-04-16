using UnityEngine;

public class FireballData : MonoBehaviour, IFireballData
{
    [Header("Movement")]
    [SerializeField] private bool isActive;
    [SerializeField] private float speed = 20f;

    [Header("Homing")]
    [SerializeField] private float timeToFullHoming;
    [SerializeField] private float maxHomingSpeed;

    [Header("Boundaries")]
    [SerializeField] private float maxHorizontalBoundaries;
    [SerializeField] private float maxVerticalBoundary;

    [Header("Damage")]
    [SerializeField] private int damage = 5;
    [SerializeField] private float impact = 500f;

    [Header("Radius")]
    [SerializeField] private float collisionRadius = 1.2f;
    [SerializeField] private float explosionRadius = 15f;
    [Range(1,3)]
    [SerializeField] private int raycastDetail = 1;

    public float Speed => speed;
    public float MaxHomingSpeed => maxHomingSpeed;
    public float CollisionRadius => collisionRadius;
    public float ExplosionRadius => explosionRadius;
    public int Detail => raycastDetail;

    public int Damage => damage;
    public float Impact => impact;

    public float MaxHorizontalBoundary => maxHorizontalBoundaries;
    public float MaxVerticalBoundary => maxVerticalBoundary;

    public Vector3 Position { get { return transform.position; } set { transform.position = value; } }
    public Quaternion Rotation { get { return transform.rotation; } set { transform.rotation = value; } }
    public Vector3 Forward => transform.forward;

    public bool IsActive { get { return isActive; } set { isActive = value; } }

    public Vector3 Direction { get; set; }
    public Quaternion LookRotation { get; set; }
    public float HomingSpeed;
    public float InitialTimeToFullHoming => timeToFullHoming;
    public float TimeToFullHomingLeft { get; set; }

    public Transform owner;
    public Transform target;
}
