using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StreetPlanner
{
    public static int streets;
    public int index;
    public StreetPlanner part;
    private List<StreetPlanner> availableStreets = new List<StreetPlanner>();
    private List<StreetPlanner> streetsToBuild = new List<StreetPlanner>();

    public Vector2 coords { get; private set; }
    private UrbanPlanner urbanPlanner;

    public StreetPlanner(Vector2 coords, UrbanPlanner urbanPlanner)
    {
        this.coords = coords;
        this.urbanPlanner = urbanPlanner;
    }

    public void Lock()
    {
        index = -1;
        //Debug.Log("LOCKED " + coords);
    }

    public void Build()
    {
        streets++;
        index = streets;
        Debug.Log(index);

        availableStreets = urbanPlanner.GetAvailableStreets(coords);
        //Debug.Log(index + " available streets " + availableStreets.Count);

        if (availableStreets.Count > 0)
        {
            SplitUp();
            BuildAdjacentStreets();
        }
    }

    private void SplitUp()
    {
        for (int i = 0; i < availableStreets.Count; i++)
        {
            StreetPlanner street = availableStreets[i];
            street.Lock();
            streetsToBuild.Add(street);
        }
    }

    private void BuildAdjacentStreets()
    {
        foreach (StreetPlanner street in availableStreets)
            street.Build();
    }
}
