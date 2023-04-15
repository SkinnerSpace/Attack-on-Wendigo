using UnityEngine;

namespace Character
{
    public class DashSFXPlayer : MonoBehaviour
    {
        [Header("Required Components")]
        [SerializeField] private PlayerCharacter player;
        [SerializeField] private CharacterData data;

        [Header("Audio References")]
        [SerializeField] private FMODUnity.EventReference snowSlideSFX;
        [SerializeField] private FMODUnity.EventReference dashSwingSFX;

        private AudioPlayer snowSlideAudioPlayer;
        private AudioPlayer dashSwingAudioPlayer;

        private void Awake()
        {
            snowSlideAudioPlayer = AudioPlayer.Create(snowSlideSFX).WithPitch(-1f, 1f).WithVariety(3);
            dashSwingAudioPlayer = AudioPlayer.Create(dashSwingSFX).WithPitch(-1f, 1f).WithVariety(3);
        }

        private void Start()
        {
            player.GetController<DashController>().onDash += Play;
            player.GetController<JumpController>().onJump += StopSlideSFX;
        }

        private void Play()
        {
            dashSwingAudioPlayer.PlayOneShot();

            if (data.IsGrounded){
                snowSlideAudioPlayer.PlayOneShot();
            }
        }

        private void StopSlideSFX(){
            snowSlideAudioPlayer.Stop();
        }
    }
}