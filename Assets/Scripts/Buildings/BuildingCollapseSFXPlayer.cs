using UnityEngine;

public class BuildingCollapseSFXPlayer : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference collapseSFX;
    [SerializeField] private CollapseController controller;

    private AudioPlayer collapsePlayer;

    private void Awake()
    {
        controller.SubscribeOnCollapse(Play);
        collapsePlayer = AudioPlayer.Create(collapseSFX).WithVariety(3).WithPitch(-3f, 3f).WithPosition(transform.position);
    }

    private void Play() => collapsePlayer.PlayOneShot();
}