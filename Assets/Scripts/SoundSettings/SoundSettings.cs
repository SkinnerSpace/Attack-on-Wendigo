using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider effectsSlider;
    [SerializeField] private Slider musicSlider;

    public float masterVolume { get; private set; }
    public float effectsVolume { get; private set; }
    public float musicVolume { get; private set; }

    private AudioBusMixer mixer;

    private void Start()
    {
        mixer = AudioBusMixer.Instance;

        SoundSettingsPersistence.Load(this);
        MenuEvents.current.onSubMenuEnter += SaveSettingsOnExit;

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        effectsSlider.onValueChanged.AddListener(SetEffectsVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    public void SetMasterEffectsMusic(float master, float effects, float music)
    {
        SetMasterVolume(master);
        masterSlider.SetValueWithoutNotify(masterVolume);

        SetEffectsVolume(effects);
        effectsSlider.SetValueWithoutNotify(effectsVolume);

        SetMusicVolume(music);
        musicSlider.SetValueWithoutNotify(musicVolume);
    }

    private void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        mixer.SetMasterVolume(masterVolume);
    }

    private void SetEffectsVolume(float volume)
    {
        effectsVolume = volume;
        mixer.SetEffectsVolume(effectsVolume);
    }

    private void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        mixer.SetMusicVolume(musicVolume);
    }

    private void SaveSettingsOnExit(string subMenuName)
    {
        if (subMenuName == "settings"){
            SoundSettingsPersistence.Save(this);
        }
    }
}
