using UnityEngine;

public class HelicopterInteractable : MonoBehaviour, IInteractable, ISwitchable
{
    private Helicopter helicopter;
    private Collider interactionCollider;
    private bool isUsed;

    private void Awake()
    {
        interactionCollider = GetComponent<Collider>();
        helicopter.onLanded += SwitchOn;
    }

    public void Interact(IInteractor interactor)
    {
        if (!isUsed){
            isUsed = true;
            SwitchOff();
            GameEvents.current.OnboardTheHelicopter();
        }
    }

    public void SwitchOn(){
        interactionCollider.enabled = true;
    }

    public void SwitchOff(){
        interactionCollider.enabled = false;
    }
}
