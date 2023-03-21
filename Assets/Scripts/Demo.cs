using UnityEngine;

public class Demo : MonoBehaviour
{
    [SerializeField] private Transform helicopterImp;
    [SerializeField] private HelicopterCameraPivot camPivot;

    private ILaunchable helicopter;

    private void Awake()
    {
        helicopter = helicopterImp.GetComponent<ILaunchable>();

        helicopter.Launch();
        camPivot.SetInstantly();
    }
}