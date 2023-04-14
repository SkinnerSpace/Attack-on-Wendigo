using UnityEngine;

public class Vehicle : MonoBehaviour, ISwitchable
{
    [SerializeField] private Helicopter helicopter;
    private Collider interactionCollider;


    private void Awake()
    {
        interactionCollider = GetComponent<Collider>();

        helicopter.onLanded += SwitchOn;
        helicopter.onTakeOff += SwitchOff;
    }

    public void SwitchOff()
    {
        interactionCollider.enabled = false;
    }

    public void SwitchOn()
    {
        interactionCollider.enabled = true;
    }
}