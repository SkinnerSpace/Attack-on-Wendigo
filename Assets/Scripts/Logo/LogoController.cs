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
    [SerializeField] private TitleAnimator titleAnimator;
    [SerializeField] private UpscaleAnimator upscaleAnimator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Play();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetState();
        }
    }

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
        titleAnimator.ResetState();
        upscaleAnimator.ResetState();
        faceController.SetStage(LogoAnimationStages.Idle);
    }
}