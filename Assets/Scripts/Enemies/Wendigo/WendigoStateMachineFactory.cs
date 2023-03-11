using UnityEngine;
using System;
using WendigoCharacter;

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

        IState burn = new FirebreathAttack(wendigo);
        IState cast = new FireballCast(wendigo);

        IState dead = new Dead(wendigo);

        SetArrival();
        //SetFirebreath();
        //SetFireball();
        SetMovement();
        SetDeath();

        void SetArrival()
        {
            Add(disabled, arrival, IsActive());
            Add(arrival, idle, IsArrived());
        }

        void SetFirebreath()
        {
            Add(idle, burn, ReadyToBurn());
            Add(chase, burn, ReadyToBurn());
            Add(burn, idle, FireBreathIsOver());
        }

        void SetFireball()
        {
            Add(idle, cast, ReadyToCast());
            Add(chase, cast, ReadyToCast());
            Add(cast, idle, FireballCastIsOver());
        }

        void SetMovement()
        {
            Add(idle, chase, HasTarget());
            Add(chase, idle, HasNoTarget());
        }

        void SetDeath()
        {
            Add(idle, dead, IsDead());
            Add(chase, dead, IsDead());
        }

        stateMachine.SetState(disabled);

        void Add(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        Func<bool> IsActive() => () => data.IsActive;
        Func<bool> IsArrived() => () => data.IsArrived;
        Func<bool> HasTarget() => () => data.Target != null;
        Func<bool> HasNoTarget() => () => data.Target == null;

        Func<bool> ReadyToCast() => () => data.Fireball.IsReadyToUse;
        Func<bool> FireballCastIsOver() => () => data.Fireball.IsOver;

        Func<bool> ReadyToBurn() => () => data.Firebreath.IsReadyToUse;
        Func<bool> FireBreathIsOver() => () => data.Firebreath.IsOver;

        Func<bool> IsDead() => () => !wendigo.Data.Health.IsAlive;

        return stateMachine;
    }
}