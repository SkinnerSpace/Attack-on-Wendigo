using System;

public interface IStateMachine
{
    void Tick();
    void SetState(IState state);
    void AddTransition(IState from, IState to, Func<bool> predicate);
}
