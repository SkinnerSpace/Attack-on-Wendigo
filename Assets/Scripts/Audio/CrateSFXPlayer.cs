using System.Collections.Generic;
using UnityEngine;

public class CrateSFXPlayer : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference dropSFX;
    [SerializeField] private FMODUnity.EventReference bumpSFX;
    [SerializeField] private FMODUnity.EventReference flashSFX;
    [SerializeField] private FMODUnity.EventReference lightOnSFX;
    [SerializeField] private FMODUnity.EventReference openSFX;
    [SerializeField] private FMODUnity.EventReference shootSFX;
    [SerializeField] private FMODUnity.EventReference smokeSFX;

    private Dictionary<string, AudioPlayer> players = new Dictionary<string, AudioPlayer>();

    private void Awake()
    {
        players.Add("Drop", 
            AudioPlayer.Create(dropSFX).WithAnchor(transform).WithPitch(-1f, 2f));

        players.Add("Bump",
            AudioPlayer.Create(bumpSFX).WithAnchor(transform).WithPitch(-4f, 1f));

        players.Add("Flash",
            AudioPlayer.Create(flashSFX).WithAnchor(transform).WithPitch(-2f, 2f));

        players.Add("LightOn",
            AudioPlayer.Create(lightOnSFX).WithAnchor(transform).WithPitch(0f, 2f));

        players.Add("Open",
            AudioPlayer.Create(openSFX).WithAnchor(transform).WithPitch(-2f, 2f));

        players.Add("Shoot",
            AudioPlayer.Create(shootSFX).WithAnchor(transform).WithPitch(-2f, 2f));

        players.Add("Smoke",
            AudioPlayer.Create(smokeSFX).WithAnchor(transform).WithPitch(-1f, 1f));
    }


    public void PlayDrop() => Play("Drop");
    public void PlayBump() => Play("Bump");
    public void PlayFlash() => Play("Flash");
    public void PlayLightOn() => Play("LightOn");
    public void PlayOpen() => Play("Open");
    public void PlayShoot() => Play("Shoot");
    public void PlaySmoke() => Play("Smoke");

    private void Play(string sfx) => players[sfx].PlayOneShot();
}

