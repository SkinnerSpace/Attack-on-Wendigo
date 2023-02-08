using UnityEngine;

public class FootstepsSFXPlayer : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private PlayerHorizontalMover horizontalMovement;
    [SerializeField] private PlayerGroundDetector groundDetector;

    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference snowFootstepSFX;
    [SerializeField] private FMODUnity.EventReference concreteFootStepSFX;

    [Header("Settings")]
    [SerializeField] private float stepSpacing = 5f;
    [SerializeField] private int variety = 5;
    [SerializeField] private float minPitch = -2f;
    [SerializeField] private float maxPitch = 2f;

    private float stepProgress;
    private bool firstStepIsMade = false;

    private AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = AudioPlayer.Create(snowFootstepSFX).WithPitch(minPitch, maxPitch).WithVariety(variety);
    }

    private void Update()
    {
        bool isGrounded = groundDetector.isGrounded;
        float distance = horizontalMovement.velocity.magnitude * Time.deltaTime;
        //Debug.Log(horizontalMovement.velocity.magnitude);

        Walk(isGrounded, distance);
        PlaySFXIfStepped();
    }

    private void Walk(bool isGrounded, float distance)
    {
        Debug.Log("WALK " + distance);

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
            audioPlayer.PlayOneShot();
        }
    }
}
