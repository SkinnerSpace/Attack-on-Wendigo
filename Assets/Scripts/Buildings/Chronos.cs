using UnityEngine;

public class Chronos : MonoBehaviour, IChronos
{
    public float DeltaTime => Time.deltaTime;
}

