using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Mark
{
    public const int MAX_NEIGHBOURS = 8;
    public static List<Mark>[] marksSortedByNeighboursCount { get; private set; } = new List<Mark>[5];

    public PropTypes Type { get; private set; }
    public int Index { get; private set; }

    // Position
    public Cell mapPosition { get; private set; }
    public Vector3 worldPosition { get; private set; }

    // Neighbours
    private CellNeighbours neighbours;

    private Transform container;

    public int neighboursCount => neighbours.count;

    public Mark() { }

    public Mark(PropTypes Type, int Index, int x, int y, float scale, Transform container)
    {
        this.Type = Type;
        this.Index = Index;

        mapPosition = new Cell(x, y);
        worldPosition = (container.position + new Vector3(x, 0f, y)) * scale;
        this.container = container;
    }

    public bool IsEmpty()
    {
        return Type == PropTypes.NONE && Index == 0;
    }

    public static void ClearSortedArray()
    {
        foreach (List<Mark> marks in marksSortedByNeighboursCount){
            if (marks != null){
                marks.Clear();
            }
        }
    }

    public void SetNeighbours(CellNeighbours neighbours){
        this.neighbours = neighbours;

        if (IsEmpty()){
            InitializeArrayIfNecessary();

            marksSortedByNeighboursCount[neighbours.count].Add(this);
        }
    }

    private void InitializeArrayIfNecessary()
    {
        if (marksSortedByNeighboursCount[neighbours.count] == null){
            marksSortedByNeighboursCount[neighbours.count] = new List<Mark>();
        }
    }
}
