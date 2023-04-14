using System.Collections.Generic;

public class CellNeighbours
{
    public bool single => count == 0;
    public int count { get; private set; }
    private Dictionary<Cell.Directions, Mark> neighbours;

    public CellNeighbours(Dictionary<Cell.Directions, Mark> neighbours){
        this.neighbours = neighbours;

        foreach (Mark mark in neighbours.Values){
            if (!mark.IsEmpty()){
                count += 1;
            }
        }
    }
}
