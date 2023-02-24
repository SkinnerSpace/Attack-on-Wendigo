using UnityEngine;

public class Airdrop : MonoBehaviour
{
    [SerializeField] private DispenserStorage storage;
    [SerializeField] private GameObject pistol;

    private void Awake()
    {
        AddItems(10);
    }

    private void AddItems(int amount)
    {
        for (int i = 0; i < amount; i++)
            DropPistol();
    }

    public void DropPistol()
    {
        storage.AddAnItem(pistol);
    }
}