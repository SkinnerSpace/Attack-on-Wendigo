using UnityEngine;
using System;

public class DashInputReader : InputReader
{
    private event Action onDash;

    private void Update()
    {
        if (input.Pressed(keys.Dash)) onDash?.Invoke();
    }

    public void Subscribe(Action onDash) => this.onDash += onDash;
    public void Unsubscribe(Action onDash) => this.onDash -= onDash;
}

