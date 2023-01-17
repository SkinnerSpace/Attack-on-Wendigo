using UnityEngine;

public class Item : MonoBehaviour, IPickable
{
    public void PickUp(IOwner owner)
    {
        Debug.Log("PICKED UP");
    }
}