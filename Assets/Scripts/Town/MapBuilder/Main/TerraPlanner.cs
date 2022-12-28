using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TerraPlanner : MonoBehaviour
{
    [SerializeField] private Cartographer cartographer;

    private int width = 256;
    private int height = 256;

    private const float RANDOM_RESOLUTION = 2560f;
    private float xOrg;
    private float yOrg;

    [SerializeField] private float scale = 16f;
    private float[,] map;

    private void Update()
    {
        if (map != null)
            cartographer.DrawMap(map);
    }

    public void GenerateMap(int width, int height)
    {
        this.width = width;
        this.height = height;

        SetRandomOrigin();

        map = new float[width, height];

        for (int x=0; x < width; x++)
        {
            for (int y=0; y < height; y++)
            {
                map[x, y] = CalculateAltitude(x, y);
            }
        }
    }

    public void SetRandomOrigin()
    {
        xOrg = UnityEngine.Random.Range(-RANDOM_RESOLUTION, RANDOM_RESOLUTION);
        yOrg = UnityEngine.Random.Range(-RANDOM_RESOLUTION, RANDOM_RESOLUTION);
        Debug.Log("Origin " + "x: " + xOrg + "y: " + yOrg);
    }

    public float CalculateAltitude(int x, int y)
    {
        float xCoord = xOrg + ((float)x / width) * scale;
        float yCoord = yOrg + ((float)y / height) * scale;

        float altitude = Mathf.PerlinNoise(xCoord, yCoord);
        return altitude;
    }
}
