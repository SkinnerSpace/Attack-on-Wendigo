using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Designer : IDesigner
{
    public enum Directions
    {
        NORTH,
        NORTH_EAST,
        EAST,
        SOUTH_EAST,
        SOUTH,
        SOUTH_WEST,
        WEST,
        NORH_WEST
    }

    protected static Dictionary<Directions, Cell> directions = new Dictionary<Directions, Cell>()
    {
        { Directions.NORTH, new Cell(0,1) },
        { Directions.NORTH_EAST, new Cell(1,1) },
        { Directions.EAST, new Cell(1,0) }
    };

    public abstract Mark Design(Requirments doc);

    public Mark NorthMark(Mark[,] map, Cell cell)
    {
        return map[cell.x, cell.y + 1];
    }

    public Mark NorthEastMark(Mark[,] map, Cell cell)
    {
        return map[cell.x + 1, cell.y + 1];
    }

    public Mark EastMark(Mark[,] map, Cell cell)
    {
        return map[cell.x + 1, cell.y];
    }

    public Mark SouthEastMark(Mark[,] map, Cell cell)
    {
        return map[cell.x + 1, cell.y - 1];
    }

    public Mark SouthMark(Mark[,] map, Cell cell)
    {
        return map[cell.x, cell.y - 1];
    }

    public Mark SouthWestMark(Mark[,] map, Cell cell)
    {
        return map[cell.x - 1, cell.y - 1];
    }

    public Mark WestMark(Mark[,] map, Cell cell)
    {
        return map[cell.x - 1, cell.y];
    }

    public Mark NorthWestMark(Mark[,] map, Cell cell)
    {
        return map[cell.x - 1, cell.y + 1];
    }
}
