using UnityEngine;

public class BlackAndWhitePostProcessingController : MonoBehaviour
{
    private static int increaseSaturation = Animator.StringToHash("IncreaseSaturation");
    private static int decreaseSaturation = Animator.StringToHash("DecreaseSaturation");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        PlayerEvents.current.onDeath += DecreaseSaturation;

    }

    private void DecreaseSaturation()
    {
        animator.Play(decreaseSaturation);
    }
}