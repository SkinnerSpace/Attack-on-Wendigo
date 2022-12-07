
public interface ICommand
{
    void Execute(IObserver observer);
    void Execute();
}