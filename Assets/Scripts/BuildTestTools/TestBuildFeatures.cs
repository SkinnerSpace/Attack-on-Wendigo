using UnityEngine;

public class TestBuildFeatures : MonoBehaviour
{
    private const KeyCode TOGGLE_GUI = KeyCode.N;
    private const KeyCode TOGGLE_CAMERA = KeyCode.M;

    [SerializeField] private GUIController gUIController;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private Transform menu;

    private void Update()
    {
        if (Input.GetKeyDown(TOGGLE_GUI))
        {
            if (gUIController.mainIsHidden)
            {
                //gUIController.ShowMainImmediately();
                menu.gameObject.SetActive(true);
            }
            else
            {
                //gUIController.HideMainImmediately();
                menu.gameObject.SetActive(false);
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
