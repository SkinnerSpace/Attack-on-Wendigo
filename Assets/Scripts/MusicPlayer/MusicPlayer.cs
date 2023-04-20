using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference battleSoundtrack;

    private AudioPlayer battlePlayer;

    private void Awake(){
        battlePlayer = AudioPlayer.Create(battleSoundtrack).WithUnpausableMode();
    }

    private void Start(){
        GameEvents.current.onFirstWeaponPickedUp += PlayBattleSoundtrack;
        GameEvents.current.onPlayerHasDied += StopBattleSoundtrack;
        GameEvents.current.onVictory += StopBattleSoundtrack;
    }

    public void PlayBattleSoundtrack(){
        battlePlayer.PlayLoop();
    }

    public void StopBattleSoundtrack(){
        battlePlayer.Stop();
    }
}
