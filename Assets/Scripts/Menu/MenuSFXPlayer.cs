using UnityEngine;

public class MenuSFXPlayer : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference openMenuSFX;
    [SerializeField] private FMODUnity.EventReference closeMenuSFX;

    [SerializeField] private FMODUnity.EventReference buttonSelectSFX;
    [SerializeField] private FMODUnity.EventReference buttonClickSFX;

    private AudioPlayer openMenuPlayer;
    private AudioPlayer closeMenuPlayer;

    private AudioPlayer buttonSelectPlayer;
    private AudioPlayer buttonClickPlayer;

    private void Awake()
    {
        openMenuPlayer = AudioPlayer.Create(openMenuSFX);
        closeMenuPlayer = AudioPlayer.Create(closeMenuSFX);

        buttonSelectPlayer = AudioPlayer.Create(buttonSelectSFX);
        buttonClickPlayer = AudioPlayer.Create(buttonClickSFX);
    }

    public void PlayMenuOpen() => openMenuPlayer.PlayOneShot();
    public void PlayMenuClose() => closeMenuPlayer.PlayOneShot();
    public void PlayButtonSelect() => buttonSelectPlayer.PlayOneShot();
    public void PlayButtonClick() => buttonClickPlayer.PlayOneShot();
}
