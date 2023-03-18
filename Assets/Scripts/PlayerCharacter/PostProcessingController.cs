using Character;
using UnityEngine;

public class PostProcessingController : MonoBehaviour, IBurnObserver
{
    private static int wentOutTrigger = Animator.StringToHash("WentOut");
    private static int damageTrigger = Animator.StringToHash("Damage");

    [SerializeField] private PlayerCharacter player;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        player.GetController<CharacterBurnController>().Subscribe(this);
    }

    public void CoolDown()
    {
        animator.ResetTrigger(damageTrigger);
        animator.SetTrigger(wentOutTrigger);
    }

    public void Scorch() => animator.SetTrigger(damageTrigger);
}
