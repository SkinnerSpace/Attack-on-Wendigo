using System.Collections.Generic;
using UnityEngine;

public class Airdrop : MonoBehaviour, IAirdrop
{
    private const int FIRST_ITEM = 0;

    [SerializeField] private DispenserStorage storage;
    [SerializeField] private CargoItem[] catalog;
    [SerializeField] private AirDropConfig config;

    private int maxConcurrentCargoCountOnTheMap = 1;
    private int currentCargoCountOnTheMap;

    private void Start()
    {
        GameEvents.current.onIntroIsOver += () => AddCargoFromCatalog(FIRST_ITEM);
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
            int catalogIndex = GetRandomWeightedCargoIndex(catalog);
            AddCargoFromCatalog(catalogIndex);
        }
    }

    private int GetRandomWeightedCargoIndex(CargoItem[] untestedCargo)
    {
        int weightSum = 0;

        for (int i = 0; i < untestedCargo.Length; ++i)
        {
            weightSum += 1;//untestedCargo[i].weight;
        }

        int index = 0;
        int lastIndex = untestedCargo.Length - 1;

        while (index < lastIndex)
        {
            if (CheckProbability(weightSum, index)){
                return index;
            }

            index++;
            weightSum -= 1;// untestedCargo[index++].weight;
        }

        return index;
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
        string cargo = catalog[index].poolName;
        storage.AddAnItem(cargo);
    }

    private void LogCargoCount(int cargoCount)
    {
        //Debug.Log("Cargo " + cargoCount);
    }

    private bool CheckProbability(int weightSum, int index) => Rand.Range(0, weightSum) < 1;//catalog[index].weight;
}
