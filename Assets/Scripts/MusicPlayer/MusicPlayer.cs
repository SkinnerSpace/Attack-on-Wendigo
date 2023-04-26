using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FMODUnity.EventReference ambientSoundtrack;
    [SerializeField] private FMODUnity.EventReference battleSoundtrack;
    [SerializeField] private FMODUnity.EventReference funeralSoundtrack;
    [SerializeField] private FMODUnity.EventReference victorySoundtrack;

    private AudioPlayer ambientPlayer;
    private AudioPlayer battlePlayer;
    private AudioPlayer funeralPlayer;
    private AudioPlayer victoryPlayer;

    private FunctionTimer timer;

    private void Awake(){
        timer = GetComponent<FunctionTimer>();

        battlePlayer = AudioPlayer.Create(battleSoundtrack).WithUnpausableMode();
        ambientPlayer = AudioPlayer.Create(ambientSoundtrack).WithUnpausableMode();
        funeralPlayer = AudioPlayer.Create(funeralSoundtrack).WithUnpausableMode();
        victoryPlayer = AudioPlayer.Create(victorySoundtrack).WithUnpausableMode();
    }

    private void Start()
    {
        ConnectSoundtracks();
        ambientPlayer.PlayLoop();
    }

    private void ConnectSoundtracks()
    {
        ConnectAmbient();
        ConnectBattleSoundtrack();
        ConnectFuneralSoundtrack();
        ConnectVictorySoundtrack();
    }

    private void ConnectAmbient(){
        GameEvents.current.onFirstWeaponPickedUp += () => ambientPlayer.Stop();
        GameEvents.current.onPlayerHasDied += () => ambientPlayer.Stop();
        GameEvents.current.onVictory += () => ambientPlayer.Stop();
    }

    private void ConnectBattleSoundtrack(){
        GameEvents.current.onFirstWeaponPickedUp += () => battlePlayer.PlayLoop();
        GameEvents.current.onPlayerHasDied += () => battlePlayer.Stop();
        GameEvents.current.onVictory += () => battlePlayer.Stop();
    }

    private void ConnectFuneralSoundtrack(){
        GameEvents.current.onPlayerHasDied += () => funeralPlayer.PlayLoop();
        GameEvents.current.onRestart += () => funeralPlayer.Stop();
    }

    private void ConnectVictorySoundtrack(){
        GameEvents.current.onVictory += () => PlayVictorySoundtrack();
        GameEvents.current.onRestart += () => victoryPlayer.Stop();
    }

    private void PlayVictorySoundtrack()
    {
        timer.Set("Victory", 1.5f, () => victoryPlayer.PlayLoop());
    }
}
