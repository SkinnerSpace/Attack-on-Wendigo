using UnityEngine;

public class BlizzardPostProcessingController : MonoBehaviour
{
    private static int intensity = Animator.StringToHash("BlizzardAnimation");

    [SerializeField] private Blizzard blizzard;
    [SerializeField] private Animator animator;

    private float additionalInfluence;

    private void Update()
    {
        float value = blizzard.Influence + additionalInfluence;
        value = Mathf.Clamp01(value);

        animator.Play(intensity, 0, value);
    }

    public void SetAdditionalInfluence(float additionalInfluence) => this.additionalInfluence = additionalInfluence;
}
