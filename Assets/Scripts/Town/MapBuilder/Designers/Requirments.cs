using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Requirments
{
    public Map Map { get; private set; }
    public Cell Cell { get; private set; }
    public Mark Mark { get; private set; }
    public float Likelyhood { get; private set; }

    public Requirments(Map Map, Cell Cell, Mark Mark, float Likelyhood)
    {
        this.Map = Map;
        this.Cell = Cell;
        this.Mark = Mark;
        this.Likelyhood = Likelyhood;
    }
}
