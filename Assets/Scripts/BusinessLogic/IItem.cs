public interface IItem
{
    void SetReady(bool isReady);
    void InitializeOnTake(ICharacterData characterData, IInputReader inputReader, IInteractionController interactor);
}