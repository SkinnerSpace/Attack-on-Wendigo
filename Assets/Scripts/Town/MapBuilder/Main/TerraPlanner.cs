using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TerraPlanner : MonoBehaviour
{
    [SerializeField] private Cartographer cartographer;

    private int width = 10;
    private int height = 10;

    private const float RANDOM_RESOLUTION = 2560f;
    private float xOrg;
    private float yOrg;

    private float scale = 100f;
    private GeoMap geoMap;

    private void Awake()
    {
        GenerateMap();
    }

    private void Update()
    {
        cartographer.DrawMap(geoMap);
    }

    public void GenerateMap()
    {
        SetRandomOrigin();

        geoMap = new GeoMap(width, height);

        for (int x=0; x < width; x++)
        {
            for (int y=0; y < height; y++)
            {
                float altitude  = CalculateAltitude(x, y);
                Cell cell = new Cell(x, y);

                geoMap.SetAltitude(cell, altitude);
            }
        }
    }

    public void SetRandomOrigin()
    {
        xOrg = UnityEngine.Random.Range(-RANDOM_RESOLUTION, RANDOM_RESOLUTION);
        yOrg = UnityEngine.Random.Range(-RANDOM_RESOLUTION, RANDOM_RESOLUTION);
    }

    public float CalculateAltitude(int x, int y)
    {
        float xCoord = xOrg + ((float)x / width) * scale;
        float yCoord = yOrg + ((float)y / height) * scale;

        float altitude = Mathf.PerlinNoise(xCoord, yCoord);
        return altitude;
    }
}
