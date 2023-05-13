using UnityEngine;

public abstract class LogoCommand : MonoBehaviour
{
    public bool IsDone => isDone;
    protected bool isDone;

    public abstract void Execute();
}
