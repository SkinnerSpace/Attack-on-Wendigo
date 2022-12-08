using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Requirments
{
    public Mark[,] map { get; private set; }
    public Cell cell { get; private set; }
    public Mark mark { get; private set; }
    public float likelyhood { get; private set; }

    public Requirments(Mark[,] map, Cell cell, Mark mark, float likelyhood)
    {
        this.map = map;
        this.cell = cell;
        this.mark = mark;
        this.likelyhood = likelyhood;
    }
}
