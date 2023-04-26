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
        GameEvents.current.onGameBegun += () => AddCargo(0);
        GameEvents.current.onFirstWeaponPickedUp += LoadCargo;
        GameEvents.current.onWeaponSweptAway += WeaponDisappeared;
        GameEvents.current.onProgressUpdate += UpdateProgress;
    }

    public void WeaponDisappeared()
    {
        cargoCount -= 1;
        LoadCargo();

        LogCargoCount(cargoCount);
    }

    public void UpdateProgress(float progress)
    {
        maxCargoCount = Mathf.RoundToInt(config.weaponCount.Evaluate(progress));
        LoadCargo();
    }

    private void LoadCargo()
    {
        int dropCount = maxCargoCount - cargoCount;

        for (int i = 0; i < dropCount; i++){
            AddCargo(Rand.Range(0, cargo.Count));
        }
    }

    public void AddCargo(int index)
    {
        cargoCount += 1;
        storage.AddAnItem(cargo[index]);

        LogCargoCount(cargoCount);
    }

    private void LogCargoCount(int cargoCount)
    {
        Debug.Log("Cargo " + cargoCount);
    }
}