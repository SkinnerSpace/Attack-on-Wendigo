using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoController : MonoBehaviour
{
    [Header("Required components")]
    [SerializeField] private BoltsController boltsController;
    [SerializeField] private LogoFaceController faceController;
    [SerializeField] private LogoPulseController pulseController;
    [SerializeField] private ShrinkController shrinkController;
    [SerializeField] private RainbowController rainbowController;

    public void Play()
    {
        ResetState();
        boltsController.Play();
    }

    public void ResetState()
    {
        boltsController.Stop();
        pulseController.Stop();
        shrinkController.Stop();
        rainbowController.Stop();
        faceController.SetStage(LogoAnimationStages.Idle);
    }
}
