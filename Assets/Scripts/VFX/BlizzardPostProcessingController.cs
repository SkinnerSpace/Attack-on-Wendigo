using UnityEngine;

public class BlizzardPostProcessingController : MonoBehaviour
{
    private static int intensity = Animator.StringToHash("BlizzardAnimation");

    [SerializeField] private Blizzard blizzard;
    [SerializeField] private Animator animator;

    private void Update()
    {
        animator.Play(intensity, 0, blizzard.Influence);
    }
}
