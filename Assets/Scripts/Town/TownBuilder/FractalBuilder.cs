using System.Collections.Generic;
using UnityEngine;

public class FractalBuilder : IFractalBuilder, IHost
{
    public int depth { get; private set; }
    public int length { get; private set; }
    public int width { get; private set; }

    private FractalArea[,] areas;
    private List<FractalArea> definedAreas = new List<FractalArea>();
    private float areaSize;
    public int areasCount { get; private set; }
    public int definedAreasCount { get; private set; }

    private FractalMediator mediator;

    public FractalBuilder(BuilderBootstrap bootstrap)
    {
        depth = bootstrap.depth;
        length = bootstrap.length;
        width = bootstrap.width;
        areaSize = bootstrap.areaSize;

        mediator = new FractalMediator(this);

        GenerateAreas();
        GetCentralArea().Define();
    }

    private void GenerateAreas()
    {
        areas = new FractalArea[length, width];
        areasCount = length * width;

        for (int x=0; x<areas.GetLength(0); x++)
        {
            for (int y=0; y<areas.GetLength(1); y++)
            {
                CreateArea(x, y);
            }
        }
    }

    private void CreateArea(int x, int y)
    {
        AreaBootstrap areaBootstrap = new AreaBootstrap();
        areaBootstrap.depth = depth;
        areaBootstrap.coords = new Vector2(x, y);
        areaBootstrap.mediator = mediator;

        areas[x, y] = new FractalArea(areaBootstrap);
    }

    public void AreaIsDefined(FractalArea area)
    {
        definedAreasCount++;
        definedAreas.Add(area);

        if (definedAreasCount >= areasCount)
            RenderDefinedAreas();
    }

    public void RenderDefinedAreas()
    {
        RawAreaData rawData = new RawAreaData();
        rawData.length = length;
        rawData.width = width;
        rawData.size = areaSize;

        AreasRenderer areasRenderer = new AreasRenderer(definedAreas, rawData);
    }

    private FractalArea GetCentralArea()
    {
        return areas[length / 2, width / 2];
    }

    public FractalArea GetArea(int x, int y)
    {
        return areas[x, y];
    }

    public void ComeIn(IFractalVisitor visitor)
    {
        foreach (IGuest guest in definedAreas)
            guest.MeetVisitor(visitor);
    }
}
