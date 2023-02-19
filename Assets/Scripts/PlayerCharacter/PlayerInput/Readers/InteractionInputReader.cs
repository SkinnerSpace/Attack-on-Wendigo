using System;

public class InteractionInputReader : InputReader
{
    private event Action onInteract;

    private void Update()
    {
        if (input.Pressed(keys.Interact)) onInteract?.Invoke();
    }

    public void Subscribe(IInteractor interactor) => onInteract += interactor.Interact;
    public void Unsubscribe(IInteractor interactor) => onInteract -= interactor.Interact;
}