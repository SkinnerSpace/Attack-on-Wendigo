using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Architect : MonoBehaviour
{
    [SerializeField] private int size;
    [SerializeField] private float scale;
    
    private DesignDepartment designDepartment;
    [SerializeField] private Builder builder;

    private Mark[,] map;

    private void Awake()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);

        designDepartment = new DesignDepartment();
        GenerateMap();
    }

    private void Start()
    {
        AddMarks(new Mark(Types.BUILDING, 1), likelyhood: 0.02f);
        AddMarks(new Mark(Types.BUILDING, 2), likelyhood: 0.02f);
        AddMarks(new Mark(Types.BUILDING, 3), likelyhood: 0.02f);
        AddMarks(new Mark(Types.BUILDING, 4), likelyhood: 0.02f);
        AddMarks(new Mark(Types.TREE, 1), likelyhood: 0.4f);

        Blueprint blueprint = DrawBlueprint();
        builder.Build(blueprint);
        ShowMap();
    }

    private void GenerateMap()
    {
        map = new Mark[size, size];

        for (int y=0; y < size; y++)
        {
            for (int x=0; x < size; x++)
            {
                map[x, y] = new Mark(0,0);
            }
        }
    }

    public void AddMarks(Mark mark, float likelyhood)
    {
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
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

    private void ShowMap()
    {
        string elements = "";

        for (int y = 0; y < size; y++)
        {
            string yElements = "";

            for (int x = 0; x < size; x++)
            {
                yElements += map[x, y].ToString() + "   ";
            }

            elements += "\n" + yElements;
        }

        Debug.Log(elements);
    }
}
