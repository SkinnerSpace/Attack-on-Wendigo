using UnityEngine;

public class ArchitectData
{
    private const float BUILDING_CHANCE = 0.05f;

    [SerializeField] private bool debugMode;

    [SerializeField] private int mapSize;
    [SerializeField] private int forestSize;
    [SerializeField] private int townSize;
    [SerializeField] private float scale;
    private Vector3 center;

    private DesignDepartment designDepartment;
    [SerializeField] private Builder builder;
}