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
        int X = doc.Cell.X;
        int Y = doc.Cell.Y;
        float chance = UnityEngine.Random.Range(0f, 1f);
        float likelyhood = CalculateLikelyhood(doc);

        return (chance <= likelyhood) ? doc.Mark : doc.Map.GetMark(X, Y);
    }

    private float CalculateLikelyhood(Requirments doc)
    {
        Dictionary<Cell.Directions, Mark> neighbours = MapNeighbourFinder.GetNeighbours(doc.Map, doc.Cell);
        float percent = GetNeighboursOfSameTypePercent(neighbours, doc.Mark.Type);
        float likelyhood = doc.Likelyhood + percent;

        return likelyhood;
    }

    private float GetNeighboursOfSameTypePercent(Dictionary<Cell.Directions, Mark> neighbours, PropTypes type)
    {
        float percent = (float) GetNeighboursMatchTypeCount(neighbours, type) / neighbours.Count;
        return percent;
    }

    private int GetNeighboursMatchTypeCount(Dictionary<Cell.Directions, Mark> neighbours, PropTypes expectedType)
    {
        int count = 0;

        foreach (Mark neighbour in neighbours.Values)
            count = (neighbour.Type == expectedType) ? (count + 1) : (count);

        return count;
    }
}
