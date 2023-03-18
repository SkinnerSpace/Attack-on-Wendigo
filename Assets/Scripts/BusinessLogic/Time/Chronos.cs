using UnityEngine;

public class Chronos : MonoBehaviour, IChronos
{
    public float DeltaTime => Time.deltaTime;

    public bool IsTicking => Time.timeScale > 0f;
}
