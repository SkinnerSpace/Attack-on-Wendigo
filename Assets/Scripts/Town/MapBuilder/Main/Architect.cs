using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Architect : MonoBehaviour
{
    [SerializeField] private int size;
    [SerializeField] private int townSize;
    [SerializeField] private float scale;
    private int townOffset;
    private Vector3 center;

    private DesignDepartment designDepartment;
    [SerializeField] private Builder builder;
    [SerializeField] private CentralizedObjects centralizedObjects;

    private Mark[,] map;

    private void Awake()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);

        designDepartment = new DesignDepartment();
        GenerateMap();
        FindBoundaries();
        centralizedObjects.Centralize(center);
    }

    private void Start()
    {
        AddMarks(new Mark(PropTypes.BUILDING, 1), likelyhood: 0.02f, townOffset);
        AddMarks(new Mark(PropTypes.BUILDING, 2), likelyhood: 0.02f, townOffset);
        AddMarks(new Mark(PropTypes.BUILDING, 3), likelyhood: 0.02f, townOffset);
        AddMarks(new Mark(PropTypes.BUILDING, 4), likelyhood: 0.02f, townOffset);
        AddMarks(new Mark(PropTypes.TREE, 1), likelyhood: 0.4f);

        Blueprint blueprint = DrawBlueprint();
        builder.Build(blueprint);
    }

    private void GenerateMap()
    {
        map = new Mark[size, size];

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                map[x, y] = new Mark(0, 0);
            }
        }

    }

    private void FindBoundaries()
    {
        float totalSize = size * scale;
        center = new Vector3(totalSize / 2, 0, totalSize / 2);
        townOffset = (size - townSize) / 2;
    }

    public void AddMarks(Mark mark, float likelyhood)
    {
        AddMarks(mark, likelyhood, 0);
    }

    public void AddMarks(Mark mark, float likelyhood, int offset)
    {
        for (int y = offset; y < size - offset; y++)
        {
            for (int x = offset; x < size - offset; x++)
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
        Blueprint blueprint = new Blueprint(map, scale, transform);
        return blueprint;
    }
}
