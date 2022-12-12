using UnityEngine;

public abstract class RigController : MonoBehaviour
{
    public abstract void Notify(RigPoint rigPoint);
    public abstract void AddRigPoint(RigPoint rigPoint);
}