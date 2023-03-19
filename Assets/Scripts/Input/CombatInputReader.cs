using System;

public class CombatInputReader : InputReader
{
    private event Action onHold;
    private event Action onPress;
    private event Action<bool> onAim;

    private void Update() => ReadInputWhenActive();

    protected override void ReadInput()
    {
        if (input.Pressed(keys.Shoot)) onPress?.Invoke();
        if (input.Hold(keys.Shoot)) onHold?.Invoke();

        if (input.Pressed(keys.Aim)) onAim?.Invoke(true);
        if (input.Released(keys.Aim)) onAim?.Invoke(false);
    }

    public void Subscribe(IWeapon observer)
    {
        onPress += observer.PressTheTrigger;
        onHold += observer.HoldTheTrigger;
        onAim += observer.Aim;
    }
    public void Unsubscribe(IWeapon observer)
    {
        onPress -= observer.PressTheTrigger;
        onHold -= observer.HoldTheTrigger;
        onAim -= observer.Aim;
    }
}
