﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private Provider provider;

    public void Build(Blueprint blueprint)
    {
        for (int y = 0; y < blueprint.map.GetLength(0); y++)
        {
            for (int x = 0; x < blueprint.map.GetLength(1); x++)
            {
                BuildObject(blueprint, new Cell(x, y), blueprint.map[x, y]);
            }
        }
    }

    private void BuildObject(Blueprint blueprint, Cell cell, Mark mark)
    {
        if (!mark.IsEmpty())
        {
            Vector3 position = (blueprint.field.position + new Vector3(cell.X, 0f, cell.Y)) * blueprint.scale;
            Instantiate(provider.GetObject(mark), position, Quaternion.identity, blueprint.field);
        }
    }
}