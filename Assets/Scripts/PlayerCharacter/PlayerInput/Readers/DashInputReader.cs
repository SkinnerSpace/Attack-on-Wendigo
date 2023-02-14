using UnityEngine;
using System;

public class DashInputReader : InputReader
{
    private event Action onDash;

    private void Update()
    {
        if (input.Pressed(keys.Dash)) onDash?.Invoke();
    }

    public void Subscribe(IDashController observer) => onDash += observer.Dash;
    public void Unsubscribe(IDashController observer) => onDash -= observer.Dash;
}

