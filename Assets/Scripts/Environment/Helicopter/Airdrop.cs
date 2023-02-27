﻿using System.Collections.Generic;
using UnityEngine;

public class Airdrop : MonoBehaviour
{
    [SerializeField] private DispenserStorage storage;
    [SerializeField] private int maxCargoCount = 5;
    [SerializeField] private List<string> cargo = new List<string>();

    private int cargoCount;

    private void Awake()
    {
        Pickable.SubscribeOnFirstPickUp(LoadCargo);
        WeaponSweeper.SubscribeOnSweptAway(CargoDisappeared);
    }

    private void LoadCargo()
    {
        int dropCount = maxCargoCount - cargoCount;

        for (int i = 0; i < dropCount; i++)
            AddCargo(Rand.Range(0, cargo.Count));
    }

    public void AddCargo(int index)
    {
        cargoCount += 1;
        storage.AddAnItem(cargo[index]);
    }

    public void CargoDisappeared()
    {
        cargoCount -= 1;
        LoadCargo();
    }
}