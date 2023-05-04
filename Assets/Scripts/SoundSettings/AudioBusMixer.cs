using UnityEngine;

public class AudioBusMixer : MonoBehaviour
{
    private const string META_MASTER_BUS = "bus:/";
    private const string MASTER_BUS = "bus:/Master";
    private const string SFX_BUS = "bus:/Master/SFX";
    private const string MUSIC_BUS = "bus:/Master/Music";

    private FMOD.Studio.Bus metaMasterBus;
    private FMOD.Studio.Bus masterBus;
    private FMOD.Studio.Bus effectsBus;
    private FMOD.Studio.Bus musicBus;

    public static AudioBusMixer Instance;

    private void Awake()
    {
        metaMasterBus = FMODUnity.RuntimeManager.GetBus(META_MASTER_BUS);
        masterBus = FMODUnity.RuntimeManager.GetBus(MASTER_BUS);
        effectsBus = FMODUnity.RuntimeManager.GetBus(SFX_BUS);
        musicBus = FMODUnity.RuntimeManager.GetBus(MUSIC_BUS);

        Instance = this;
    }

    public void SetMetaMasterVolume(float volume) => metaMasterBus.setVolume(volume);
    public void SetMasterVolume(float volume) => masterBus.setVolume(volume / 100f);
    public void SetEffectsVolume(float volume) => effectsBus.setVolume(volume / 100f);
    public void SetMusicVolume(float volume) => musicBus.setVolume(volume / 100f);
}