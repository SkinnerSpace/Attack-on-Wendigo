using UnityEngine;

public class TestBuildFeatures : MonoBehaviour
{
    private const KeyCode TOGGLE_GUI = KeyCode.N;
    private const KeyCode TOGGLE_CAMERA = KeyCode.M;

    [SerializeField] private GUIController gUIController;
    [SerializeField] private CameraManager cameraManager;

    private void Update()
    {
        if (Input.GetKeyDown(TOGGLE_GUI))
        {
            if (gUIController.mainIsHidden)
            {
                gUIController.ShowMainImmediately();
            }
            else
            {
                gUIController.HideMainImmediately();
            }
        }

        if (Input.GetKeyDown(TOGGLE_CAMERA))
        {
            if (cameraManager.isTrackingTheHelicopter)
            {
                cameraManager.TrackTheCharacter();
                PlayerEvents.current.UpdateMoverReadiness(true);
            }
            else
            {
                cameraManager.TrackTheHelicopter();
                PlayerEvents.current.UpdateMoverReadiness(false);
            }
        }
    }
}