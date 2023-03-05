using UnityEngine;
using System;

public static class WendigoStateMachineFactory
{
    public static IStateMachine Create(Wendigo wendigo)
    {
        WendigoData data = wendigo.Data;

        IStateMachine stateMachine = new StateMachine();

        IState disabled = new Disabled();
        IState arrival = new Arrival(wendigo);
        IState idle = new Idle(wendigo);
        IState chase = new Chase(wendigo);
        IState cast = new FireballCast(wendigo);
        IState dead = new Dead(wendigo);

        Add(disabled, arrival, IsActive());
        Add(arrival, idle, IsArrived());

        Add(idle, cast, ReadyToShoot());
        Add(chase, cast, ReadyToShoot());

        Add(idle, chase, HasTarget());
        Add(chase, idle, HasNoTarget());

        Add(cast, idle, FireballCastIsOver());

        Add(idle, dead, IsDead());
        Add(chase, dead, IsDead());

        stateMachine.SetState(disabled);

        void Add(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        Func<bool> IsActive() => () => data.IsActive;
        Func<bool> IsArrived() => () => data.IsArrived;
        Func<bool> HasTarget() => () => data.Target != null;
        Func<bool> HasNoTarget() => () => data.Target == null;
        Func<bool> ReadyToShoot() => () => data.IsReadyToShoot;
        Func<bool> FireballCastIsOver() => () => data.FireballCastIsOver;
        Func<bool> IsDead() => () => !wendigo.Data.IsAlive;

        return stateMachine;
    }
}