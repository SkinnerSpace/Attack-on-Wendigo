using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class LogoController : MonoBehaviour
{
    private LogoPushController pushController;
    private LogoAnimatedPart[] parts;

    private void OnEnable()
    {
        pushController = GetComponentInChildren<LogoPushController>();
        parts = GetComponentsInChildren<LogoAnimatedPart>();
    }

    public void SetStage(LogoAnimationStages stage)
    {
        pushController.Push();

        foreach (LogoAnimatedPart part in parts){
            part.SetFrame(stage);
        }
    }
}


