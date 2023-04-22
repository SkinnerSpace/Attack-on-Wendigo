using UnityEngine;

public static class SoundSettingsPersistence
{
    private const string MASTER_VOLUME = "MasterVolume";
    private const string EFFECTS_VOLUME = "EffectsVolume";
    private const string MUSIC_VOLUME = "MusicVolume";

    private const int MAX_VOLUME = 100;

    public static void Save(SoundSettings settings)
    {
        PlayerPrefs.SetFloat(MASTER_VOLUME, settings.masterVolume);
        PlayerPrefs.SetFloat(EFFECTS_VOLUME, settings.effectsVolume);
        PlayerPrefs.SetFloat(MUSIC_VOLUME, settings.musicVolume);
    }

    public static void Load(SoundSettings settings)
    {
        settings.masterVolume = PlayerPrefs.HasKey(MASTER_VOLUME) ? PlayerPrefs.GetFloat(MASTER_VOLUME) : MAX_VOLUME;
        settings.effectsVolume = PlayerPrefs.HasKey(EFFECTS_VOLUME) ? PlayerPrefs.GetFloat(EFFECTS_VOLUME) : MAX_VOLUME;
        settings.musicVolume = PlayerPrefs.HasKey(MUSIC_VOLUME) ? PlayerPrefs.GetFloat(MUSIC_VOLUME) : MAX_VOLUME;

        settings.UpdateSliders();
    }
}
