using System.Collections.Generic;
using UnityEngine;

public class PlayerBloodParticlesSoundSystem : MonoBehaviour
{
    [SerializeField] private List<ParticleSoundSystem> soundSystems;

    private void Start()
    {
        PlayerEvents.current.onDeath += Play;
    }

    private void Play()
    {
        foreach (ParticleSoundSystem soundSystem in soundSystems){
            soundSystem.SwitchOn();
        }
    }
}
