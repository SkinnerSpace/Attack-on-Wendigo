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

        float likelyhood = AdjustLikelyhood(doc.likelyhood);
        CheckNeighbours(doc);

        return (chance <= likelyhood) ? doc.mark : doc.map[x, y];
    }

    private float AdjustLikelyhood(float originalLikelyhood)
    {
        float adjustedLikelyhood = originalLikelyhood;
        
        return originalLikelyhood;
    }

    private void CheckNeighbours(Requirments doc)
    {
        foreach (Cell direction in directions.Values)
        {
            Cell guessedCell = new Cell(doc.cell.x + direction.x, doc.cell.y + direction.y);

            // OUT OF BOUNDS
            if (!CellExist(guessedCell, doc.map))
                Debug.Log("Out " + new Vector2(guessedCell.x, guessedCell.y));

        }
    }

    private bool CellExist(Cell guessedCell, Mark[,] map)
    {
        return (guessedCell.x > -1 && guessedCell.x < map.GetLength(0)) && (guessedCell.y > -1 && guessedCell.y < map.GetLength(1));
    }
}
