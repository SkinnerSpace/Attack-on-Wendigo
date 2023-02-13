using UnityEngine;

public class Chronos : IChronos
{
    public float DeltaTime => Time.deltaTime;
}

