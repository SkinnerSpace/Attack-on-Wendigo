using UnityEngine;

public class FootstepsSFXPlayer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private FMODUnity.EventReference snowFootstepSFX;
    [SerializeField] private FMODUnity.EventReference concreteFootStepSFX;
    [SerializeField] private float stepSpacing = 5f;

    [Header("Required components")]
    [SerializeField] private PlayerHorizontalMovement horizontalMovement;
    [SerializeField] private PlayerGroundDetector groundDetector;

    private AudioPlayer audioPlayer = new AudioPlayer();

    private float stepProgress;
    private bool firstStepIsMade = false;

    private void Update()
    {
        bool isGrounded = groundDetector.isGrounded;
        float distance = horizontalMovement.velocity.magnitude * Time.deltaTime;

        Walk(isGrounded, distance);
        PlaySFXIfStepped();
    }

    private void Walk(bool isGrounded, float distance)
    {
        if (isGrounded && distance > 0f)
        {
            MakeAStep(distance);
        }
        else
        {
            Interrupt();
        }
    }

    private void MakeAStep(float distance)
    {
        stepProgress = firstStepIsMade ? (stepProgress + distance) : stepSpacing;
        firstStepIsMade = true;
    }

    private void Interrupt()
    {
        firstStepIsMade = false;
        stepProgress = 0f;
    }

    private void PlaySFXIfStepped()
    {
        if (stepProgress >= stepSpacing)
        {
            stepProgress -= stepSpacing;
            audioPlayer.Play(snowFootstepSFX);
        }
    }
}
