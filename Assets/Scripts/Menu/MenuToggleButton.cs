using System;

public class MenuToggleButton : MenuElement
{
    public event Action onToggle;

    public void Toggle() => onToggle?.Invoke();
}
