using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform cameraHolderImp;

    [Header("Pivots")]
    [SerializeField] private Transform characterPivot;
    [SerializeField] private Transform helicopterPivot;

    private ICameraHolder cameraHolder;
    public bool isTrackingTheHelicopter { get; private set; }

    private void Start()
    {
        GameEvents.current.onIntroIsOver += TrackTheCharacter;
        HelicopterEvents.current.onIsGoingToSetOff += TrackTheHelicopterFlyingAway;

        TrackTheHelicopter();
    }

    private void Awake()
    {
        cameraHolder = cameraHolderImp.GetComponent<ICameraHolder>();
    }

    public void TrackTheCharacter()
    {
        isTrackingTheHelicopter = false;
        cameraHolder.SetGameMode(characterPivot);
    }

    public void TrackTheHelicopter()
    {
        isTrackingTheHelicopter = true;
        cameraHolder.SetDemoMode(helicopterPivot);
    }

    public void TrackTheHelicopterFlyingAway() => cameraHolder.SetOutroMode(helicopterPivot);
}
