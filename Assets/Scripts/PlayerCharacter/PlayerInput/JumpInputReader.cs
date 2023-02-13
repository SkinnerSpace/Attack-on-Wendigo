using System;
using UnityEngine;

public class JumpInputReader : InputReader
{
    private event Action onJump;
    private event Action onStop;

    private void Update()
    {
        if (input.Hold(keys.Jump)) onJump?.Invoke();
        if (input.Released(keys.Jump)) onStop?.Invoke();
    }

    public void Subscribe(JumpController observer)
    {
        onJump += observer.OnJump;
        onStop += observer.OnStop;
    }

    public void Unsubscribe(JumpController observer)
    {
        onJump -= observer.OnJump;
        onStop -= observer.OnStop;
    }
}
