using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Cell
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Cell(int X, int Y)
    {
        this.X = X;
        this.Y = Y;
    }
}
