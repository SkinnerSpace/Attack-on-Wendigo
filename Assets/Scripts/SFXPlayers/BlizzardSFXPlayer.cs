using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlizzardSFXPlayer : MonoBehaviour
{
    [SerializeField] private Blizzard blizzard;
    [SerializeField] private Transform attenuationPoint;

    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference blizzardSFX;

    //private IAttenuationPoint attenuationPoint;
    private AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = AudioPlayer.Create(blizzardSFX);
        UpdateAttenuation();

        audioPlayer.PlayLoop();
    }

    //public void SetAttenuationPoint(IAttenuationPoint attenuationPoint) => this.attenuationPoint = attenuationPoint;

    private void Update() => UpdateAttenuation();

    private void UpdateAttenuation()
    {
        float distToCenter = Vector3.Distance(transform.position, attenuationPoint.position);
        float volume = Mathf.InverseLerp(0f, blizzard.Radius, distToCenter);

        audioPlayer.WithVolume(volume).UpdateParameters();
    }
}
