using System;

public class JumpInputReader : InputReader
{
    private event Action onJump;

    private void Update()
    {
        if (input.Hold(keys.Jump)) onJump?.Invoke();
    }

    public void Subscribe(JumpController observer) => onJump += observer.Jump;
    public void Unsubscribe(JumpController observer) => onJump -= observer.Jump;
}