using UnityEngine;

namespace Character
{
    public class JumpSFXPlayer : MonoBehaviour, IJumpObserver
    {
        [Header("Required Components")]
        [SerializeField] private PlayerCharacter player;
        [SerializeField] private CharacterData data;

        [Header("Audio References")]
        [SerializeField] private FMODUnity.EventReference offTheSurfaceSFX;

        private AudioPlayer offTheSurfacePlayer;

        private void Awake()
        {
            offTheSurfacePlayer = AudioPlayer.Create(offTheSurfaceSFX).WithPitch(-2f, 2f).WithVariety(3);
        }

        private void Start()
        {
            player.GetController<JumpController>().Subscribe(this);
        }

        public void OnJump()
        {
            offTheSurfacePlayer.WithPosition(data.Bottom).PlayOneShot();
        }
    }
}