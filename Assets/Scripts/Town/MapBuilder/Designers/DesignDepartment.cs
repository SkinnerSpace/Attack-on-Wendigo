﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DesignDepartment : IDesigner
{
    readonly private UrbanPlanner urbanPlanner = new UrbanPlanner();
    readonly private LandscapeDesigner landscapeDesigner = new LandscapeDesigner();

    public Mark Design(Requirments doc)
    {
        switch (doc.Mark.Type)
        {
            case PropTypes.BUILDING:
                return urbanPlanner.Design(doc);

            case PropTypes.TREE:
                return landscapeDesigner.Design(doc);

            default:
                return new Mark();
        }
    }
}