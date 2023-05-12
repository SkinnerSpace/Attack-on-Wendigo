using UnityEngine;

[ExecuteAlways]
public class LogoAnimatedPart : MonoBehaviour
{
    private LogoAnimationFrame[] frames;

    private void OnEnable()
    {
        frames = GetComponentsInChildren<LogoAnimationFrame>(true);
    }

    public void SetFrame(LogoAnimationStages stage)
    {
        for (int i = 0; i < frames.Length; i++)
        {
            if (frames[i].stage == stage){
                frames[i].SwitchOn();
            }
            else{
                frames[i].SwitchOff();
            }
        }
    }
}

