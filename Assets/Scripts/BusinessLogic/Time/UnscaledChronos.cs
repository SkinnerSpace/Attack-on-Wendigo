using UnityEngine;

public class UnscaledChronos : Chronos
{
    public override float DeltaTime => Time.unscaledDeltaTime;
}
