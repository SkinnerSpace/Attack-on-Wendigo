using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UrbanPlanner : MonoBehaviour
{
    private StreetPlanner[,] grid;
    [SerializeField] private int townLength;
    [SerializeField] private int townWidth;

    private void Awake()
    {
        GenerateGrid();
        GetCentralStreet().Build();
        //ShowGrid();
    }

    private void GenerateGrid()
    {
        grid = new StreetPlanner[townLength, townWidth];
        
        for (int i=0; i<grid.GetLength(0); i++)
        {
            for (int j=0; j<grid.GetLength(1); j++)
            {
                grid[i, j] = new StreetPlanner(new Vector2(i, j), this);
            }
        }
    }

    private StreetPlanner GetCentralStreet()
    {
        return grid[townLength / 2, townWidth / 2];
    }

    public List<StreetPlanner> GetAvailableStreets(Vector2 coords)
    {
        List<StreetPlanner> availableStreets = new List<StreetPlanner>();

        AddStreet(coords + Vector2.up, availableStreets);
        AddStreet(coords + Vector2.down, availableStreets);
        AddStreet(coords + Vector2.left, availableStreets);
        AddStreet(coords + Vector2.right, availableStreets);

        return availableStreets;
    }

    private void AddStreet(Vector2 coords, List<StreetPlanner> availableStreets)
    {
        int x = (int)coords.x;
        int y = (int)coords.y;

        if (StreetExist(x, y))
        {
            StreetPlanner street = grid[x, y];
            AddStreetIfAvailable(street, availableStreets);
        }
    }

    private bool StreetExist(int x, int y)
    {
        return (x >= 0 && x < grid.GetLength(0)) && (y >= 0 && y < grid.GetLength(1));
    }

    private void AddStreetIfAvailable(StreetPlanner street, List<StreetPlanner> availableStreets)
    {
        if (street.index == 0)
            availableStreets.Add(street);
    }

    private void ShowGrid()
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Debug.Log(grid[i, j]);
            }
        }
    }
}
