public interface IInputReader
{
    T Get<T>() where T : InputReader;
}
