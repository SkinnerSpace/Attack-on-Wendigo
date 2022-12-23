using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LandscapeDesigner : Designer
{
    public override Mark Design(Requirments doc)
    {
        int x = doc.Cell.X;
        int y = doc.Cell.Y;
        float chance = UnityEngine.Random.Range(0f, 1f);

        return (chance <= doc.Likelyhood) ? doc.Mark : doc.Map[x, y];
    }
}
