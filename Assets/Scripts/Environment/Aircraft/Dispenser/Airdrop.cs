using System.Collections.Generic;
using UnityEngine;

public class Airdrop : MonoBehaviour, IAirdrop
{
    private const int FIRST_ITEM = 0;

    [SerializeField] private DispenserStorage storage;
    [SerializeField] private List<string> cargoCatalog = new List<string>();
    [SerializeField] private AirDropConfig config;

    private int maxConcurrentCargoCountOnTheMap = 1;
    private int currentCargoCountOnTheMap;

    private void Start()
    {
        GameEvents.current.onIntroIsOver += () => AddCargoFromCatalog(FIRST_ITEM);
        GameEvents.current.onFirstWeaponPickedUp += LoadDeficientCargo;
        GameEvents.current.onCargoIsTaken += OnCargoUnpacked;
        GameEvents.current.onProgressUpdate += UpdateProgress;
    }

    public void OnCargoUnpacked()
    {
        currentCargoCountOnTheMap -= 1;
        LoadDeficientCargo();
        LogCargoCount(currentCargoCountOnTheMap);
    }

    public void UpdateProgress(float progress)
    {
        maxConcurrentCargoCountOnTheMap = Mathf.RoundToInt(config.weaponCount.Evaluate(progress));
        LoadDeficientCargo();
    }

    private void LoadDeficientCargo()
    {
        int deficientCargoCount = maxConcurrentCargoCountOnTheMap - currentCargoCountOnTheMap;

        for (int i = 0; i < deficientCargoCount; i++){
            int catalogIndex = Rand.Range(0, cargoCatalog.Count);
            AddCargoFromCatalog(catalogIndex);
        }
    }

    public void AddCargoFromCatalogWithQuantity(int index, int count)
    {
        for (int i = 0; i < count; i++){
            AddCargoFromCatalog(index);
        }
    }

    public void AddCargoFromCatalog(int index)
    {
        currentCargoCountOnTheMap += 1;
        storage.AddAnItem(cargoCatalog[index]);

        LogCargoCount(currentCargoCountOnTheMap);
    }

    private void LogCargoCount(int cargoCount)
    {
        //Debug.Log("Cargo " + cargoCount);
    }
}