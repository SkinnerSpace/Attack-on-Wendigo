using System.Collections.Generic;

public static class MapNeighbourFinder
{
    private const int EDGE = -1;

    public static void InitializeNeighbours(Map map, int size)
    {
        int offset = Ruler.GetCustomOffset(size, map);
        Mark.ClearSortedArray();

        for (int y = offset; y < map.size - offset; y++){
            for (int x = offset; x < map.size - offset; x++){
                Cell cell = new Cell(x, y);
                CellNeighbours neighbours = new CellNeighbours(GetFourNeighbours(map, cell));
                map.GetMark(cell).SetNeighbours(neighbours);
            }
        }
    }

    public static Dictionary<Cell.Directions, Mark> GetEightNeighbours(Map map, Cell cell)
    {
        Dictionary<Cell.Directions, Mark> neighbours = new Dictionary<Cell.Directions, Mark>();

        foreach (Cell.Directions direction in Cell.eightDirectionOffsets.Keys){
            FindANeighbourOnTheMapByCellAndDirection(neighbours, map, cell, direction);
        }

        return neighbours;
    }

    public static Dictionary<Cell.Directions, Mark> GetFourNeighbours(Map map, Cell cell)
    {
        Dictionary<Cell.Directions, Mark> neighbours = new Dictionary<Cell.Directions, Mark>();

        foreach (Cell.Directions direction in Cell.fourDirectionOffsets.Keys){
            FindANeighbourOnTheMapByCellAndDirection(neighbours, map, cell, direction);
        }

        return neighbours;
    }

    private static void FindANeighbourOnTheMapByCellAndDirection(Dictionary<Cell.Directions, Mark> neighbours, Map map, Cell cell, Cell.Directions direction)
    {
        Cell offset = Cell.eightDirectionOffsets[direction];
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
