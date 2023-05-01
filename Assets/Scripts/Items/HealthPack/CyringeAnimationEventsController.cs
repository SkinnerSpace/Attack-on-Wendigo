using UnityEngine;

public class CyringeAnimationEventsController : MonoBehaviour
{
    [SerializeField] private Drugs drugs;
    [SerializeField] private HealingLiquid liquid;
    [SerializeField] private bool isActive;

    public void Inject(float time)
    {
        if (isActive){
            liquid.Inject(time);
            drugs.Apply();
        }
    }

    public void InjectionIsOver()
    {
        if (isActive){
            drugs.DisposeOffUsedDrugs();
        }
    }
}