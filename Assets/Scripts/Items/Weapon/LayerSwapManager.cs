using UnityEngine;

public class LayerSwapManager : MonoBehaviour
{
    [SerializeField] private Pickable pickable;
    private LayerChanger[] layerChangers;

    private void Awake()
    {
        layerChangers = GetComponentsInChildren<LayerChanger>();
        pickable.onInteract += SwapTheLayers;
    }

    public void SwapTheLayers()
    {
        foreach (LayerChanger changer in layerChangers)
            changer.SwapTheLayer();
    }
}


