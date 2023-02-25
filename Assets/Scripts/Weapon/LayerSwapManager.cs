using UnityEngine;

public class LayerSwapManager : MonoBehaviour, IPickableObserver
{
    [SerializeField] private Pickable pickable;
    private LayerChanger[] layerChangers;

    private void Awake()
    {
        layerChangers = GetComponentsInChildren<LayerChanger>();
        pickable.Subscribe(this);
    }

    public void SwapTheLayers()
    {
        foreach (LayerChanger changer in layerChangers)
            changer.SwapTheLayer();
    }

    public void OnPickedUp(bool pickedUp) => SwapTheLayers();
}


