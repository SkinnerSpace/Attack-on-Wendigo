using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Cell
{
    public int x { get; private set; }
    public int y { get; private set; }

    public Cell(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
