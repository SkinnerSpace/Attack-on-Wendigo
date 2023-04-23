using UnityEngine;

public class AudioBusMixer : MonoBehaviour
{
    private const string MASTER_BUS = "bus:/";
    private const string SFX_BUS = "bus:/SFX";
    private const string MUSIC_BUS = "bus:/Music";

    private FMOD.Studio.Bus masterBus;
    private FMOD.Studio.Bus effectsBus;
    private FMOD.Studio.Bus musicBus;

    public static AudioBusMixer Instance;

    private void Awake()
    {
        masterBus = FMODUnity.RuntimeManager.GetBus(MASTER_BUS);
        effectsBus = FMODUnity.RuntimeManager.GetBus(SFX_BUS);
        musicBus = FMODUnity.RuntimeManager.GetBus(MUSIC_BUS);

        Instance = this;
    }

    public void SetMasterVolume(float volume) => masterBus.setVolume(volume / 100f);
    public void SetEffectsVolume(float volume) => effectsBus.setVolume(volume / 100f);
    public void SetMusicVolume(float volume) => musicBus.setVolume(volume / 100f);
}