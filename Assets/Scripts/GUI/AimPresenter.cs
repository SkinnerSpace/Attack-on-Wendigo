using UnityEngine;

public class AimPresenter : MonoBehaviour
{
    [SerializeField] private Aim aim;
    [SerializeField] private MainController controller;

    private VisionTrigger visionTrigger;

    private void Start()
    {
        visionTrigger = new VisionTrigger();

    }

    public void OnUpdate(VisionTarget target)
    {
        
    }

    public void OnTargetUpdate(VisionTarget target)
    {
        throw new System.NotImplementedException();
    }
}
