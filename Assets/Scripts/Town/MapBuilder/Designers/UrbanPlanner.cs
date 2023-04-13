using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UrbanPlanner : Designer
{
    private const int EDGE = -1;

    public override Mark Design(Requirments doc)
    {
        int X = doc.Cell.X;
        int Y = doc.Cell.Y;
        float chance = UnityEngine.Random.Range(0f, 1f);
        float likelyhood = CalculateLikelyhood(doc);

        return (chance <= likelyhood) ? doc.Mark : doc.Map.GetMark(X, Y);
    }

    private float CalculateLikelyhood(Requirments doc)
    {
        Dictionary<Directions, Mark> neighbours = GetNeighbours(doc);
        float percent = GetNeighboursOfSameTypePercent(neighbours, doc.Mark.Type);
        float likelyhood = doc.Likelyhood + percent;

        return likelyhood;
    }

    private Dictionary<Directions, Mark> GetNeighbours(Requirments doc)
    {
        Dictionary<Directions, Mark> neighbours = new Dictionary<Directions, Mark>();

        foreach (Directions direction in directions.Keys)
        {
            Cell offset = directions[direction];
            Cell guessedCell = new Cell(doc.Cell.X + offset.X, doc.Cell.Y + offset.Y);

            if (CellExist(guessedCell, doc.Map))
                neighbours.Add(direction, doc.Map.GetMark(guessedCell.X, guessedCell.Y));
        }

        return neighbours;
    }

    private bool CellExist(Cell guessedCell, Map Map)
    {
        return (guessedCell.X > EDGE && guessedCell.X < Map.GetWidth()) && 
               (guessedCell.Y > EDGE && guessedCell.Y < Map.GetHeight());
    }

    private float GetNeighboursOfSameTypePercent(Dictionary<Directions, Mark> neighbours, PropTypes type)
    {
        float percent = (float) GetNeighboursMatchTypeCount(neighbours, type) / neighbours.Count;
        return percent;
    }

    private int GetNeighboursMatchTypeCount(Dictionary<Directions, Mark> neighbours, PropTypes expectedType)
    {
        int count = 0;

        foreach (Mark neighbour in neighbours.Values)
            count = (neighbour.Type == expectedType) ? (count + 1) : (count);

        return count;
    }
}
