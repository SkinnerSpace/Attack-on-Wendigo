using UnityEngine;

public class ResetLogoCommand : LogoCommand
{
    [SerializeField] private BoltsController boltsController;
    [SerializeField] private LogoFaceController faceController;

    override public void Execute()
    {
        boltsController.Stop();
        faceController.SetStage(LogoAnimationStages.Idle);
    }
}