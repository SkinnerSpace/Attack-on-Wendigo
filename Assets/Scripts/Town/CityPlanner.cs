using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CityPlanner : MonoBehaviour
{
    [SerializeField] private Vector2 townSize;
    [SerializeField] private float roadWidth = 4f;
    [SerializeField] private float pathWidth = 2f;

    [SerializeField] private Vector2 minCellSize;
    [SerializeField] private Vector2 maxCellSize;

    private void Awake()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        Vector2 townCenter = townSize / 2f;
        GenerateArea(new Vector2(0, 0));
        /*
        GenerateNE();
        GenerateES();
        GenerateSW();
        GenerateWN();
        */
    }

    private void GenerateArea(Vector2 direction)
    {
        Vector2 overallSpace = townSize / 4f;
        Vector2 availableSpace = overallSpace;
        Vector2 occupiedSpace = Vector2.zero;
        int divisions = 0;
        float distance = 1f;

        Vector2 cellSize = Vector2.Lerp(maxCellSize, minCellSize, distance);

        while (availableSpace.x > 0f)
        {
            divisions += 1;
            occupiedSpace += cellSize;
            availableSpace = overallSpace - occupiedSpace;

            distance = (availableSpace.x * availableSpace.y) / (overallSpace.x * overallSpace.y);
            cellSize = Vector2.Lerp(minCellSize, maxCellSize, distance);

            Debug.Log("divisions " + divisions);
            Debug.Log("available splace " + availableSpace);
            Debug.Log("distance " + distance);
            Debug.Log("Cell size " + cellSize);
        }

        
    }
}
