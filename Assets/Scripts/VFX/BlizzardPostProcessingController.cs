using UnityEngine;

public class BlizzardPostProcessingController : MonoBehaviour
{
    private static int intensity = Animator.StringToHash("BlizzardAnimation");

    [SerializeField] private Blizzard blizzard;
    [SerializeField] private Animator animator;

    private float influenceMultiplier;

    private void Update()
    {
        float value = blizzard.Influence + influenceMultiplier;
        value = Mathf.Clamp01(value);

        animator.Play(intensity, 0, value);
    }

    public void SetBlizzardInfluenctMultiplier(float influenceMultiplier) => this.influenceMultiplier = influenceMultiplier;
}
