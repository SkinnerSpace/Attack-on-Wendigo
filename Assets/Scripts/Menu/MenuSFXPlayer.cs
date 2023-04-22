using System.Collections.Generic;
using UnityEngine;

public class MenuSFXPlayer : MonoBehaviour
{
    [SerializeField] private FunctionTimer timer;

    [SerializeField] private FMODUnity.EventReference openMenuSFX;
    [SerializeField] private FMODUnity.EventReference closeMenuSFX;

    [SerializeField] private FMODUnity.EventReference buttonSelectSFX;
    [SerializeField] private FMODUnity.EventReference buttonClickSFX;

    private Dictionary<string, AudioPlayer> players;
    private AudioPlayer currentPlayer;
    private bool isPlaying;

    private void Awake()
    {
        SetUpPlayers();
    }

    private void SetUpPlayers()
    {
        players = new Dictionary<string, AudioPlayer>()
        {
            { "open menu", AudioPlayer.Create(openMenuSFX).WithUnpausableMode()},
            { "close menu", AudioPlayer.Create(closeMenuSFX).WithUnpausableMode()},
            { "button select", AudioPlayer.Create(buttonSelectSFX).WithUnpausableMode()},
            { "button click", AudioPlayer.Create(buttonClickSFX).WithUnpausableMode()}
        };
    }

    public void PlayMenuOpen() => SetAndPlay("open menu");
    public void PlayMenuClose()
    {
        SetAndPlay("close menu");
        ResetPlayer();
    }
    public void PlayButtonSelect() => SetAndPlay("button select");
    public void PlayButtonClick() => SetAndPlay("button click");

    private void SetAndPlay(string name)
    {
        SetPlayer(players[name]);
        PlaySFXOnce();
    }

    private void SetPlayer(AudioPlayer player)
    {
        if (currentPlayer == null){
            currentPlayer = player;
        }
    }

    private void PlaySFXOnce(){
        if (!isPlaying){
            isPlaying = true;

            currentPlayer.PlayOneShot();
            timer.Set("ResetPlayer", 0.1f, ResetPlayer);
        }
    }

    private void ResetPlayer(){
        isPlaying = false;
        currentPlayer = null;
    }
}
