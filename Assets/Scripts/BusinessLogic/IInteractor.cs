public interface IInteractor
{
    void Interact();
    void DropAnItem();
    void TakeAnItem(IPickable pickable);
}
