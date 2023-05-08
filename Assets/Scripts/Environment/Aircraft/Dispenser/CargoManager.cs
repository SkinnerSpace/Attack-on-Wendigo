using System.Collections.Generic;
using UnityEngine;

public class CargoManager : MonoBehaviour
{
    private const int FIRST_ITEM = 0;
    [SerializeField] private AnimationCurve concurrentCargoCountCurve;
    
    private int cargoToSpawnCount;
    private int maxConcurrentCargoCountOnTheMap = 1;
    private int currentCargoCountOnTheMap;

    public static CargoManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameEvents.current.onIntroIsOver += () => CargoDirector.Instance.ReserveItem(FIRST_ITEM);
        GameEvents.current.onIntroIsOver += UpdateCargoToSpawnCount;

        GameEvents.current.onCargoIsTaken += OnCargoUnpacked;
        GameEvents.current.onDeathProgressUpdate += SetProgress;
    }

    public bool IsEmpty() => cargoToSpawnCount <= 0;

    public void OnCargoIsDropped()
    {
        cargoToSpawnCount -= 1;
        currentCargoCountOnTheMap += 1;
    }

    private void OnCargoUnpacked()
    {
        currentCargoCountOnTheMap -= 1;
        UpdateCargoToSpawnCount();
    }

    private void SetProgress(float progress)
    {
        maxConcurrentCargoCountOnTheMap = Mathf.RoundToInt(concurrentCargoCountCurve.Evaluate(progress));
        UpdateCargoToSpawnCount();
    }

    private void UpdateCargoToSpawnCount()
    {
        cargoToSpawnCount = maxConcurrentCargoCountOnTheMap - currentCargoCountOnTheMap;
    }
}
