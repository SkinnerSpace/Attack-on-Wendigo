using System.Collections.Generic;

public static class MapNeighbourFinder
{
    private const int EDGE = -1;

    public static Dictionary<Cell.Directions, Mark> GetNeighbours(Map map, Cell cell)
    {
        Dictionary<Cell.Directions, Mark> neighbours = new Dictionary<Cell.Directions, Mark>();

        foreach (Cell.Directions direction in Cell.directionOffsets.Keys){
            FindANeighbourOnTheMapByCellAndDirection(neighbours, map, cell, direction);
        }

        return neighbours;
    }

    private static void FindANeighbourOnTheMapByCellAndDirection(Dictionary<Cell.Directions, Mark> neighbours, Map map, Cell cell, Cell.Directions direction)
    {
        Cell offset = Cell.directionOffsets[direction];
        Cell guessedCell = new Cell(cell.X + offset.X, cell.Y + offset.Y);

        if (CellExist(guessedCell, map))
        {
            neighbours.Add(direction, map.GetMark(guessedCell.X, guessedCell.Y));
        }
    }

    private static bool CellExist(Cell guessedCell, Map Map)
    {
        return (guessedCell.X > EDGE && guessedCell.X < Map.GetWidth()) &&
               (guessedCell.Y > EDGE && guessedCell.Y < Map.GetHeight());
    }
}
