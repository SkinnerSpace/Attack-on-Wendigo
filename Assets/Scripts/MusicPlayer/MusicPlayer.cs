using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference ambientSoundtrack;
    [SerializeField] private FMODUnity.EventReference battleSoundtrack;

    private AudioPlayer ambientPlayer;
    private AudioPlayer battlePlayer;

    private void Awake(){
        battlePlayer = AudioPlayer.Create(battleSoundtrack).WithUnpausableMode();
        ambientPlayer = AudioPlayer.Create(ambientSoundtrack).WithUnpausableMode();
    }

    private void Start(){
        GameEvents.current.onFirstWeaponPickedUp += () => ambientPlayer.Stop();
        GameEvents.current.onFirstWeaponPickedUp += () => battlePlayer.PlayLoop();

        GameEvents.current.onPlayerHasDied += () => battlePlayer.Stop();
        GameEvents.current.onVictory += () => battlePlayer.Stop();

        ambientPlayer.PlayLoop();
    }
}
