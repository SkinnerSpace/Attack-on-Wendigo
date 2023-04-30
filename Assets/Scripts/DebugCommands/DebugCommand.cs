using System;

public class DebugCommand : DebugCommandBase
{
    private Action command;

    public DebugCommand(string id, string description, string format, Action command) : base(id, description, format)
    {
        this.command = command;
    }

    public void Invoke()
    {
        command.Invoke();
    }
}

public class DebugCommand<T> : DebugCommandBase
{
    private Action<T> command;

    public DebugCommand(string id, string description, string format, Action<T> command) : base(id, description, format)
    {
        this.command = command;
    }

    public void Invoke(T value)
    {
        command.Invoke(value);
    }
}

public class DebugCommand<T1, T2> : DebugCommandBase
{
    private Action<T1, T2> command;

    public DebugCommand(string id, string description, string format, Action<T1, T2> command) : base(id, description, format)
    {
        this.command = command;
    }

    public void Invoke(T1 firstValue, T2 secondValue)
    {
        command.Invoke(firstValue, secondValue);
    }
}

