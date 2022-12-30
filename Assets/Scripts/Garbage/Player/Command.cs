using UnityEngine;

public abstract class Command : MonoBehaviour, ICommand
{
    public void Execute(IObserver observer)
    {
        Execute();

        if (FeedBackConditions())
            observer.FeedBack(GetType());
    }

    public abstract void Execute();

    public virtual bool FeedBackConditions()
    {
        return false;
    }
}
