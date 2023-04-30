public interface IHelicopterDoor
{
    void Subscribe(IHelicopterDoorObserver observer);
    void Open();
    void Close();
    void OpenSilently();
    void CloseSilently();
}

