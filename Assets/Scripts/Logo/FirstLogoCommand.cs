using UnityEngine;

public class FirstLogoCommand : LogoCommand
{
    [SerializeField] private BoltsController boltsController;
    [SerializeField] private LogoFaceController faceController;

    override public void Execute()
    {
        isDone = false;
        faceController.SetStage(LogoAnimationStages.Idle);

    }

    private void Pierce()
    {
        faceController.SetStage(LogoAnimationStages.Pierced);
        isDone = true;
    }
}
