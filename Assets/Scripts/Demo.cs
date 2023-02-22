using UnityEngine;

public class Demo : MonoBehaviour
{
    [SerializeField] private Helicopter helicopter;
    [SerializeField] private HelicopterCameraPivot camPivot;

    private void Awake()
    {
        helicopter.Launch();
        camPivot.SetInstantly();
    }
}