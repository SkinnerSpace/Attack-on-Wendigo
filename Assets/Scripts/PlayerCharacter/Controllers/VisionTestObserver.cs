using UnityEngine;

public class VisionTestObserver : MonoBehaviour, IVisionObserver
{
    [SerializeField] private MainController main;

    private void Start()
    {
        main.GetController<VisionController>().Subscribe(this);
    }

    public void OnUpdate(VisionTarget target)
    {
        Debug.Log(target.Transform.name);
    }
}