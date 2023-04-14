using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct Cell
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

    public static Dictionary<Directions, Cell> directionOffsets = new Dictionary<Directions, Cell>()
    {
        { Directions.NORTH, new Cell(0,1) },
        { Directions.NORTH_EAST, new Cell(1,1) },
        { Directions.EAST, new Cell(1,0) },
        { Directions.SOUTH_EAST, new Cell(1,-1) },
        { Directions.SOUTH, new Cell(0,-1) },
        { Directions.SOUTH_WEST, new Cell(-1,-1) },
        { Directions.WEST, new Cell(-1, 0) },
        { Directions.NORH_WEST, new Cell(-1,1) }
    };

    public int X { get; private set; }
    public int Y { get; private set; }

    public Cell(int X, int Y)
    {
        this.X = X;
        this.Y = Y;
    }
}
