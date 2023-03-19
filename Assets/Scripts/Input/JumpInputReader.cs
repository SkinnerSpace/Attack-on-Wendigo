using System;
using UnityEngine;

public class JumpInputReader : InputReader
{
    private event Action onJump;
    private event Action onStop;

    private void Update() => ReadInputWhenActive();

    protected override void ReadInput(){
        if (input.Hold(keys.Jump)) onJump?.Invoke();
        if (input.Released(keys.Jump)) onStop?.Invoke();
    }

    public void Subscribe(IJumpController observer)
    {
        onJump += observer.TryToJump;
        onStop += observer.Stop;
    }

    public void Unsubscribe(IJumpController observer)
    {
        onJump -= observer.TryToJump;
        onStop -= observer.Stop;
    }
}
