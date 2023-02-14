using System;

public class CombatInputReader : InputReader
{
    private event Action onAttack;
    private event Action<bool> onAim;

    private void Update()
    {
        if (input.Hold(keys.Shoot)) onAttack?.Invoke();

        if (input.Pressed(keys.Aim)) onAim?.Invoke(true);
        if (input.Released(keys.Aim)) onAim?.Invoke(false);
    }

    public void Subscribe(IWeapon observer)
    {
        onAttack += observer.PullTheTrigger;
        onAim += observer.Aim;
    }
    public void Unsubscribe(IWeapon observer)
    {
        onAttack -= observer.PullTheTrigger;
        onAim -= observer.Aim;
    }
}
