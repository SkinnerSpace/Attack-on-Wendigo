using UnityEngine;

public class WeaponAbandonmentDetector : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private WeaponSweeper sweeper;
    [SerializeField] private FunctionTimer timer;
    [SerializeField] private float abandonmentDistance = 20f;

    private ItemPhysicalBody physics;
    private ICharacterData owner;

    private bool isActive;
    private bool isGrounded = false;

    private void Awake()
    {
        physics = GetComponent<ItemPhysicalBody>();
    }

    private void Start()
    {
        weapon.SubscribeOnReady(SwitchOn);
        physics.SubscribeOnGroundUpdate(OnGroundUpdate);
    }

    private void Update()
    {
        if (isActive && isGrounded)
        {
            float distance = Vector3.Distance(transform.position, owner.Position);

            if (distance >= abandonmentDistance)
            {
                sweeper.SweepTheWeapon();
                ResetState();
            }
        }
    }

    public void Initialize(ICharacterData owner){
        this.owner = owner;
    }

    public void SwitchOn(){
        isActive = true;
    }

    public void ResetState()
    {
        isActive = false;
        isGrounded = false;
    }

    private void OnGroundUpdate(bool isGrounded){
        this.isGrounded = isGrounded;
    }
}