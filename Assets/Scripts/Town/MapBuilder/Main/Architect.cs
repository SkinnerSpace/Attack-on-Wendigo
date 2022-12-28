using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

# if UNITY_EDITOR
using UnityEditor;
# endif

public class Architect : MonoBehaviour
{
    [SerializeField] private bool debugMode;

    [SerializeField] private int mapSize;
    [SerializeField] private int forestSize;
    [SerializeField] private int townSize;
    [SerializeField] private float scale;
    private Vector3 center;

    private DesignDepartment designDepartment;
    [SerializeField] private Builder builder;
    [SerializeField] private TerraPlanner terraformer;

    [SerializeField] private CentralizedObjects centralizedObjects;

    private Mark[,] map;
    private const float BUILDING_CHANCE = 0.01f;

    private void Awake()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);

        designDepartment = new DesignDepartment();
        GenerateMap();
        FindCenter();
        ShowMap();
        centralizedObjects.Centralize(center);
    }

    private void Start()
    {
        AddMarks(new Mark(PropTypes.BUILDING, 1), likelyhood: BUILDING_CHANCE);
        AddMarks(new Mark(PropTypes.BUILDING, 2), likelyhood: BUILDING_CHANCE);
        AddMarks(new Mark(PropTypes.BUILDING, 3), likelyhood: BUILDING_CHANCE);
        AddMarks(new Mark(PropTypes.BUILDING, 4), likelyhood: BUILDING_CHANCE);
        AddMarks(new Mark(PropTypes.TREE, 1), likelyhood: 0.4f);

        Blueprint blueprint = DrawBlueprint();
        builder.Build(blueprint);

        terraformer.GenerateMap(mapSize, mapSize);
    }

    private void GenerateMap()
    {
        map = new Mark[mapSize, mapSize];

        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                map[x, y] = new Mark(PropTypes.NONE, Index: 0);
            }
        }

    }

    private void FindCenter()
    {
        float totalSize = mapSize * scale;
        center = new Vector3(totalSize / 2, 0, totalSize / 2);
    }

    public void AddMarks(Mark mark, float likelyhood)
    {
        int offset = GetOffset(mark.Type);

        for (int y = offset; y < mapSize - offset; y++)
        {
            for (int x = offset; x < mapSize - offset; x++)
            {
                if (map[x, y].IsEmpty())
                {
                    Requirments doc = new Requirments(map, new Cell(x, y), mark, likelyhood);
                    map[x, y] = designDepartment.Design(doc);
                }
            }
        }
    }

    private Blueprint DrawBlueprint()
    {
        int offset = GetOffset(PropTypes.TREE);
        Blueprint blueprint = new Blueprint(map, offset, scale, transform);
        return blueprint;
    }

    public int GetOffset(PropTypes type)
    {
        switch (type)
        {
            case PropTypes.BUILDING:
                return (mapSize - townSize) / 2;

            case PropTypes.TREE:
                return (mapSize - forestSize) / 2;

            default:
                return 0;
        }
    }

    # if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!debugMode) return;

        FindCenter();

        float forestFullSize = (forestSize * scale);
        Vector3 forestCubeSize = new Vector3(forestFullSize, 20f, forestFullSize);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, forestCubeSize);

        float townFullSize = (townSize * scale);
        Vector3 townCubeSize = new Vector3(townFullSize, 20f, townFullSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(center, townCubeSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, (forestFullSize / 2) * 0.8f);

        float mapFullSize = (mapSize * scale);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(center, mapFullSize / 2);
    }
#endif

    private void ShowMap()
    {
        string elements = "";

        for (int y = 0; y < mapSize; y++)
        {
            string yElements = "";

            for (int x = 0; x < mapSize; x++)
            {
                yElements += map[x, y].ToString() + "   ";
            }

            elements += "\n" + yElements;
        }

        Debug.Log(elements);
    }
}
