using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DesignDepartment : IDesigner
{
    private UrbanPlanner urbanPlanner = new UrbanPlanner();
    private LandscapeDesigner landscapeDesigner = new LandscapeDesigner();

    public Mark Design(Requirments doc)
    {
        switch (doc.mark.type)
        {
            case Types.BUILDING:
                return urbanPlanner.Design(doc);

            case Types.TREE:
                return landscapeDesigner.Design(doc);

            default:
                return new Mark();
        }
    }
}
