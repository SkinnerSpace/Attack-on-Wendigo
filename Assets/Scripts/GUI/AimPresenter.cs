using UnityEngine;

public class AimPresenter : MonoBehaviour, IVisionObserver
{
    [Header("Required Components")]
    [SerializeField] private VisionBehavior visionBehavior;
    [SerializeField] private VisionEvaluator visionEvaluator;
    [SerializeField] private Aim aim;

    private void Start()
    {
        visionBehavior.Subscribe(this);
    }

    public void OnUpdate(VisionTarget target)
    {
        if (visionEvaluator.TargetIsSuitable(target)) aim.SetOnTarget();
        else aim.SetOffTarget();
    }
}
