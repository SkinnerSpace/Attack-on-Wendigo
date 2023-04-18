using Character;
using UnityEngine;

public class PostProcessingController : MonoBehaviour, IBurnObserver
{
    private static int wentOutTrigger = Animator.StringToHash("WentOut");
    private static int damageTrigger = Animator.StringToHash("Damage");

    [SerializeField] private PlayerCharacter player;
    [SerializeField] private ScorchSFXPlayer scorchSFXPlayer;

    private Animator animator;

    private bool isBurning;

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
        //isBurning = false;

        animator.ResetTrigger(damageTrigger);
        animator.SetTrigger(wentOutTrigger);

        scorchSFXPlayer.PlayCoolDownSFX();
    }

    public void Scorch()
    {
        /*if (!isBurning){
            isBurning = true;
            scorchSFXPlayer.PlayInflameSFX();
        }
        else{
            scorchSFXPlayer.PlayDamageSFX();
        }*/

        scorchSFXPlayer.PlayDamageSFX();
        animator.SetTrigger(damageTrigger);
    }
}
