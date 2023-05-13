using UnityEngine;

public abstract class LogoComponentController : MonoBehaviour
{
    protected bool isDone;

    public abstract void Play();
    public bool IsWorking() => !isDone;
}