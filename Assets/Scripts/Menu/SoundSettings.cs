using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider effectsSlider;
    [SerializeField] private Slider musicSlider;

    public float masterVolume;
    public float effectsVolume;
    public float musicVolume;

    private void Awake()
    {
        SoundSettingsPersistence.Load(this);
    }

    private void Start()
    {
        MenuEvents.current.onBackToSettings += () => SoundSettingsPersistence.Save(this);

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        effectsSlider.onValueChanged.AddListener(SetEffectsVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    private void SetMasterVolume(float volume)
    {
        masterVolume = volume;

    }

    private void SetEffectsVolume(float volume)
    {
        effectsVolume = volume;
    }

    private void SetMusicVolume(float volume)
    {
        musicVolume = volume;
    }

    public void UpdateSliders()
    {
        masterSlider.SetValueWithoutNotify(masterVolume);
        effectsSlider.SetValueWithoutNotify(effectsVolume);
        musicSlider.SetValueWithoutNotify(musicVolume);
    }
}
