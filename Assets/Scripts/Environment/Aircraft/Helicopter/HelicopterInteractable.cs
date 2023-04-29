using UnityEngine;

public class HelicopterInteractable : MonoBehaviour, IInteractable
{
    private bool isActivated;

    public void Interact(IInteractor interactor)
    {
        if (!isActivated){
            isActivated = true;
            GameEvents.current.OnboardTheHelicopter();
        }
    }
}
