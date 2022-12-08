using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LandscapeDesigner : Designer
{
    public override Mark Design(Requirments doc)
    {
        int x = doc.cell.x;
        int y = doc.cell.y;
        float chance = UnityEngine.Random.Range(0f, 1f);

        return (chance <= doc.likelyhood) ? doc.mark : doc.map[x, y];
    }
}
