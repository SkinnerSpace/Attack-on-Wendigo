using UnityEngine;

public class LayerChanger : MonoBehaviour
{
    [SerializeField] private Layers replacementLayer;

    public void SwapTheLayer()
    {
        int originalLayer = gameObject.layer;
        gameObject.layer = (int)replacementLayer;
        replacementLayer = (Layers)originalLayer;
    }
}