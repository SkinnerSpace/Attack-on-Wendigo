using UnityEngine;

public class HelicopterInteractable : MonoBehaviour, IInteractable, ISwitchable
{
    private Helicopter helicopter;
    private Collider[] interactionColliders;
    private bool isUsed;

    private void Awake()
    {
        helicopter = GetComponentInParent<Helicopter>();
        interactionColliders = GetComponentsInChildren<Collider>();
        helicopter.onLanded += SwitchOn;
    }

    public void Interact(IInteractor interactor)
    {
        if (!isUsed){
            isUsed = true;
            SwitchOff();
            HelicopterEvents.current.NotifyOnBoarded();
        }
    }

    public void SwitchOn(){
        foreach (Collider interactionCollider in interactionColliders){
            interactionCollider.enabled = true;
        }
    }

    public void SwitchOff(){
        foreach (Collider interactionCollider in interactionColliders){
            interactionCollider.enabled = false;
        }
    }
}
