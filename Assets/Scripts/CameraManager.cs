using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CameraHolder cam;
    [SerializeField] private Transform characterPivot;
    [SerializeField] private Transform helicopterPivot;

    public void SetLookAtTheCharacter()
    {
        cam.SetState(CameraHolder.States.Gameplay);
        cam.SetPivot(characterPivot);
    }

    public void SetLookAtTheHelicopter()
    {
        cam.SetState(CameraHolder.States.Demo);
        cam.SetPivot(helicopterPivot);
    }
}
