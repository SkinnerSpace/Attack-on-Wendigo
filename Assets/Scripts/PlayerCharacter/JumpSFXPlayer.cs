using UnityEngine;

namespace Character
{
    public class JumpSFXPlayer : MonoBehaviour
    {
        [Header("Required Components")]
        [SerializeField] private PlayerCharacter player;
        [SerializeField] private CharacterData data;

        [Header("Audio References")]
        [SerializeField] private FMODUnity.EventReference offTheSurfaceSFX;
        [SerializeField] private FMODUnity.EventReference inTheAirSFX;

        private AudioPlayer offTheSurfacePlayer;
        private AudioPlayer inTheAirPlayer;

        private void Awake()
        {
            offTheSurfacePlayer = AudioPlayer.Create(offTheSurfaceSFX).WithPitch(-2f, 2f).WithVariety(3);
            inTheAirPlayer = AudioPlayer.Create(inTheAirSFX).WithPitch(-2f, 2f);
        }

        private void Start()
        {
            player.GetController<JumpController>().onJump += OnJump;
            player.GetController<JumpController>().onSecondJump += OnSecondJump;
        }

        private void OnJump() => offTheSurfacePlayer.WithPosition(data.Bottom).PlayOneShot();
        private void OnSecondJump() => inTheAirPlayer.PlayOneShot();
    }
}