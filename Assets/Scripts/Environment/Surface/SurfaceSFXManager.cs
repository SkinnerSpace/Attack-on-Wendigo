using System.Collections.Generic;
using UnityEngine;

public class SurfaceSFXManager : MonoBehaviour
{
    private List<SurfaceHitSFXPlayer> sfxPlayers = new List<SurfaceHitSFXPlayer>();
    public SurfaceHitSFXPlayer currentSFXPlayer { get; private set; }
    float highestThreshold = 0f;

    public void SetPosition(Vector3 position)
    {
        foreach (SurfaceHitSFXPlayer sfxPlayer in sfxPlayers)
            sfxPlayer.SetPosition(position);
    }

    public void SetVolume(float volume)
    {
        foreach (SurfaceHitSFXPlayer sfxPlayer in sfxPlayers)
            sfxPlayer.SetVolume(volume);
    }

    public void ChooseSFX(float radius)
    {
        highestThreshold = 0f;

        foreach (SurfaceHitSFXPlayer sfxPlayer in sfxPlayers)
            SetCurrentSFXPlayer(radius, sfxPlayer);
    }

    private void SetCurrentSFXPlayer(float radius, SurfaceHitSFXPlayer sfxPlayer)
    {
/*        float threshold = sfxPlayer.Threshold;

        if (Crosses(radius, threshold))
        {
            highestThreshold = threshold;
            currentSFXPlayer = sfxPlayer;
        }*/
    }

    private bool Crosses(float radius, float threshold) => (radius >= threshold) && (threshold >= highestThreshold);
    public void PlaySFX() => currentSFXPlayer.Play();
    public void AddSFXPlayer(SurfaceHitSFXPlayer sfxPlayer) => sfxPlayers.Add(sfxPlayer);
}
