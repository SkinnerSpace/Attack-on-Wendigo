using UnityEngine;

public class SecondLogoCommand : LogoCommand
{
    [SerializeField] private LogoPulseController pulseController;
    [SerializeField] private LogoFaceController faceController;
    [SerializeField] private ShrinkController shrinkController;

    override public void Execute()
    {
        isDone = false;
        shrinkController.Play();

    }
}