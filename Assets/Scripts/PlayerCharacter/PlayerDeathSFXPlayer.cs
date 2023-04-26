using UnityEngine;

namespace Character
{
    public class PlayerDeathSFXPlayer : MonoBehaviour
    {
        [SerializeField] private FMODUnity.EventReference damageSFX;
        [SerializeField] private FMODUnity.EventReference smashSFX;
        [SerializeField] private FMODUnity.EventReference bleedingSFX;

        private AudioPlayer damagePlayer;
        private AudioPlayer smashAudioPlayer;
        private AudioPlayer bleedingAudioPlayer;

        private void Awake()
        {
            damagePlayer = AudioPlayer.Create(damageSFX).WithPitch(-2f, 2f).WithVariety(3);
            smashAudioPlayer = AudioPlayer.Create(smashSFX).WithPitch(-2f, 2f).WithVariety(7);
            bleedingAudioPlayer = AudioPlayer.Create(bleedingSFX).WithPitch(-1f, 1f);
        }

        private void Start()
        {
            GameEvents.current.onBluntDamageReceived += () => damagePlayer.PlayOneShot();
            GameEvents.current.onPlayerHasDied += PlayDeathSFX;
        }

        private void PlayDeathSFX()
        {
            smashAudioPlayer.PlayOneShot();
            bleedingAudioPlayer.PlayOneShot();
        }
    }
}