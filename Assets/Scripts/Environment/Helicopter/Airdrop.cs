using UnityEngine;

public class Airdrop : MonoBehaviour
{
    [SerializeField] private DispenserStorage storage;
    private IObjectPooler pooler;

    private void Start()
    {
        pooler = PoolHolder.Instance;
        AddItems(5);
    }

    private void AddItems(int amount)
    {
        for (int i = 0; i < amount; i++)
            DropPistol();
    }

    public void DropPistol()
    {
        storage.AddAnItem("Pistol");
    }
}