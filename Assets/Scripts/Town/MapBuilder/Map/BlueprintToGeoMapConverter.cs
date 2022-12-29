using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BlueprintToGeoMapConverter
{
    private Blueprint blueprint;
    private bool iterationIsOver;

    private int offset;
    private int x;
    private int y;

    private int width;
    private int height;

    public GeoMap ConvertToGeoMap(Blueprint blueprint)
    {
        PrepareForConversion(blueprint);

        GeoMap geoMap = new GeoMap(
            blueprint.GetWidth(), 
            blueprint.GetHeight());

        return geoMap;
    }

    private void PrepareForConversion(Blueprint blueprint)
    {
        this.blueprint = blueprint;

        offset = blueprint.offset;
        x = offset;
        y = offset;

        width = blueprint.map.GetLength(1) - offset;
        height = blueprint.map.GetLength(0) - offset;
    }

    public void IterateThroughMap(Mark[,] map)
    {

    }
}
