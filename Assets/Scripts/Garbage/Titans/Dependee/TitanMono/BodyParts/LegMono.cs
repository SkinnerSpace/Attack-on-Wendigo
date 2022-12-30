using UnityEngine;

public class LegMono : MonoBehaviour
{
    [SerializeField] private Sides side;

    public ILeg GetLeg()
    {
        return new Leg(side, new TransformProxy(transform));
    }
}