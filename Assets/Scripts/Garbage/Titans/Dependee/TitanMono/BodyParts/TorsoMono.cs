using UnityEngine;

public class TorsoMono : MonoBehaviour
{
    public Torso GetTorso()
    {
        return new Torso(new TransformProxy(transform));
    }
}
