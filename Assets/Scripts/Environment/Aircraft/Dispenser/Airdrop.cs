using System.Collections.Generic;
using UnityEngine;

public class Airdrop : MonoBehaviour, IAirdrop
{
    [SerializeField] private DispenserStorage storage;
    [SerializeField] private List<string> cargo = new List<string>();
    [SerializeField] private AirDropConfig config;

    private int maxCargoCount = 1;
    private int cargoCount;

    private void Start()
    {
        GameEvents.current.onFirstWeaponPickedUp += LoadCargo;
        GameEvents.current.onWeaponSweptAway += WeaponDisappeared;
        GameEvents.current.onProgressUpdate += UpdateProgress;
    }

    public void AddCargo(int index)
    {
        cargoCount += 1;
        storage.AddAnItem(cargo[index]);
    }

    public void WeaponDisappeared()
    {
        cargoCount -= 1;
        Debug.Log("Cargo after disappearance: " + cargoCount);

        LoadCargo();
    }

    public void UpdateProgress(float progress)
    {
        maxCargoCount = Mathf.RoundToInt(config.weaponCount.Evaluate(progress));
        Debug.Log("Cargo max count " + maxCargoCount);
        LoadCargo();
    }

    private void LoadCargo()
    {
        int dropCount = maxCargoCount - cargoCount;

        for (int i = 0; i < dropCount; i++){
            AddCargo(Rand.Range(0, cargo.Count));
        }

        Debug.Log("Cargo after the load " + cargoCount);
    }
}