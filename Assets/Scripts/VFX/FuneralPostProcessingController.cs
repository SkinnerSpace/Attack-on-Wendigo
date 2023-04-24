using UnityEngine;

public class FuneralPostProcessingController : MonoBehaviour
{
    private static int desaturateAnimation = Animator.StringToHash("Desaturate");

    [SerializeField] private float delay = 2f;

    private Animator animator;
    private FunctionTimer timer;

    private void Awake(){
        animator = GetComponent<Animator>();
        timer = GetComponent<FunctionTimer>();
    }

    private void Start(){
        GameEvents.current.onPlayerHasDied += Desaturate;
    }

    private void Desaturate()
    {
        timer.Set("Desaturate", delay, () => animator.Play(desaturateAnimation));
    }
}