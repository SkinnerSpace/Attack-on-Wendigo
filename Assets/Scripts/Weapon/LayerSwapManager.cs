using UnityEngine;

public class LayerSwapManager : MonoBehaviour, IPickableObserver
{
    private LayerChanger[] layerChangers;

    private void Awake()
    {
        layerChangers = GetComponentsInChildren<LayerChanger>();
        GetComponent<Pickable>().Subscribe(this);
    }

    public void SwapTheLayers()
    {
        foreach (LayerChanger changer in layerChangers)
            changer.SwapTheLayer();
    }

    public void OnPickedUp(bool pickedUp) => SwapTheLayers();
}


