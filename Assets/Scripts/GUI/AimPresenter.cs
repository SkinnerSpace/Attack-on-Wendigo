using UnityEngine;

public class AimPresenter : MonoBehaviour, IVisionObserver
{
    [SerializeField] private Aim aim;
    [SerializeField] private MainController controller;

    private void Start()
    {
        controller.GetController<VisionController>().Subscribe(this);
    }

    public void OnUpdate(VisionTarget target)
    {
        
    }
}
