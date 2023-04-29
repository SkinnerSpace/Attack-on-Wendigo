public interface IInteractionController
{
    void Interact();
    void DropAnItem();
    void TakeAnItem(IPickable pickable);
}
