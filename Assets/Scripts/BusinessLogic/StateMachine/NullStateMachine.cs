using System;

public class NullStateMachine : IStateMachine
{
    public static IStateMachine Instance
    {
        get 
        {
            if (instance == null)
                instance = new NullStateMachine();

            return instance;
        }
    }

    private static IStateMachine instance;

    public void Tick() { }
    public void SetState(IState state) { }
    public void AddTransition(IState from, IState to, Func<bool> predicate) { }
}
