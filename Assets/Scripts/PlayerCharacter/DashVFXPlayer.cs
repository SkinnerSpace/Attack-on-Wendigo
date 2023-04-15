using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class DashVFXPlayer : MonoBehaviour
    {
        [Header("Required Components")]
        [SerializeField] private PlayerCharacter player;
        [SerializeField] private CharacterData data;

        [SerializeField] private ParticleSystem swingParticles;
        [SerializeField] private ParticleSystem snowSlideParticles;

        private Dictionary<DashDirections, float> angles;
        private Dictionary<DashDirections, float> positions;

        private void Awake()
        {
            angles = new Dictionary<DashDirections, float>(){
                { DashDirections.Forward, 0f },
                { DashDirections.Backward, 0f },
                { DashDirections.Right, -45f },
                { DashDirections.Left, 45 }
            };

            positions = new Dictionary<DashDirections, float>()
            {
                { DashDirections.Forward, 0f },
                { DashDirections.Backward, 0f },
                { DashDirections.Right, 2.5f },
                { DashDirections.Left, -2.5f }
            };
        }

        private void Start()
        {
            player.GetController<DashController>().onDashAngle += Play;
            player.GetController<DashController>().onStop += StopAll;

            player.GetController<JumpController>().onJump += StopSnowParticles;
        }

        private void Play(float movementAngle, DashDirections direction)
        {
            swingParticles.transform.localEulerAngles = new Vector3(0f, angles[direction], 0f);
            swingParticles.transform.localPosition = new Vector3(positions[direction], 0f, 0f);
            swingParticles.Play();

            if (data.IsGrounded){
                snowSlideParticles.Play();
            }
        }

        private void StopAll()
        {
            swingParticles.Stop();
            StopSnowParticles();
        }

        private void StopSnowParticles(){
            snowSlideParticles.Stop();
        }
    }
}