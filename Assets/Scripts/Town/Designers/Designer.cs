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
        { Directions.EAST, new Cell(1,0) },
        { Directions.SOUTH_EAST, new Cell(1,-1) },
        { Directions.SOUTH, new Cell(0,-1) },
        { Directions.SOUTH_WEST, new Cell(-1,-1) },
        { Directions.WEST, new Cell(-1, 0) },
        { Directions.NORH_WEST, new Cell(-1,1) }
    };

    public abstract Mark Design(Requirments doc);
}
