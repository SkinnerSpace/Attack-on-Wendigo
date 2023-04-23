using UnityEngine;

public interface IKeyBinds
{
    float MouseSensitivity { get; }
    float MouseInversion { get; }
    
    KeyCode Shoot { get; }
    KeyCode Aim { get; }
    KeyCode Interact { get; }

    KeyCode MoveRight { get; }
    KeyCode MoveLeft { get; }
    KeyCode MoveForward { get; }
    KeyCode MoveBackward { get; }
    KeyCode Jump { get; }
    KeyCode Dash { get; }
}