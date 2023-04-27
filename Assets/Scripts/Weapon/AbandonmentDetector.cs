using UnityEngine;

public class AbandonmentDetector : MonoBehaviour
{
    private const float ABANDONMENT_DISTANCE = 30f;

    [SerializeField] private FunctionTimer timer;

    private ItemPhysicalBody physics;
    private ICharacterData owner;
    private IHandyItem item;
    private ItemSweeper sweeper;

    private bool isActive;
    private bool isGrounded = false;

    private void Awake()
    {
        physics = GetComponent<ItemPhysicalBody>();
        sweeper = GetComponent<ItemSweeper>();
        item = GetComponent<IHandyItem>();
    }

    private void Start()
    {
        item.SubscribeOnReady(SwitchOn);
        physics.SubscribeOnGroundUpdate(OnGroundUpdate);
    }

    private void Update()
    {
        if (isActive && isGrounded)
        {
            float distance = Vector3.Distance(transform.position, owner.Position);

            if (distance >= ABANDONMENT_DISTANCE)
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