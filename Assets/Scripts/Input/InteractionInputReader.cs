﻿using System;

public class InteractionInputReader : InputReader
{
    private event Action onInteract;

    private void Update() => ReadInputWhenActive();

    protected override void ReadInput(){
        if (input.Pressed(keys.Interact)) onInteract?.Invoke();
    }

    public void Subscribe(Action onInteract) => this.onInteract += onInteract;
    public void Unsubscribe(Action onInteract) => this.onInteract -= onInteract;
}