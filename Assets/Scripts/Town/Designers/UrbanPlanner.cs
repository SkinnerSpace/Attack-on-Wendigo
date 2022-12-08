using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UrbanPlanner : Designer
{
    public override Mark Design(Requirments doc)
    {
        int x = doc.cell.x;
        int y = doc.cell.y;
        float chance = UnityEngine.Random.Range(0f, 1f);
        float likelyhood = CalculateLikelyhood(doc);

        return (chance <= likelyhood) ? doc.mark : doc.map[x, y];
    }

    private float CalculateLikelyhood(Requirments doc)
    {
        Dictionary<Directions, Mark> neighbours = GetNeighbours(doc);
        float percent = GetNeighboursOfSameTypePercent(neighbours, doc.mark.type);
        float likelyhood = doc.likelyhood + percent;

        return likelyhood;
    }

    private Dictionary<Directions, Mark> GetNeighbours(Requirments doc)
    {
        Dictionary<Directions, Mark> neighbours = new Dictionary<Directions, Mark>();

        foreach (Directions direction in directions.Keys)
        {
            Cell offset = directions[direction];
            Cell guessedCell = new Cell(doc.cell.x + offset.x, doc.cell.y + offset.y);

            if (CellExist(guessedCell, doc.map))
                neighbours.Add(direction, doc.map[guessedCell.x, guessedCell.y]);
        }

        return neighbours;
    }

    private bool CellExist(Cell guessedCell, Mark[,] map)
    {
        return (guessedCell.x > -1 && guessedCell.x < map.GetLength(0)) && (guessedCell.y > -1 && guessedCell.y < map.GetLength(1));
    }

    private float GetNeighboursOfSameTypePercent(Dictionary<Directions, Mark> neighbours, Types type)
    {
        float percent = (float) GetNeighboursMatchTypeCount(neighbours, type) / neighbours.Count;
        return percent;
    }

    private int GetNeighboursMatchTypeCount(Dictionary<Directions, Mark> neighbours, Types expectedType)
    {
        int count = 0;

        foreach (Mark neighbour in neighbours.Values)
            count = (neighbour.type == expectedType) ? (count + 1) : (count);

        return count;
    }
}
