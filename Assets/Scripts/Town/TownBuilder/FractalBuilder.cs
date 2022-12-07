using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FractalBuilder : IHost
{
    private int layer;
    private int length;
    private int width;

    private FractalArea[,] areas;
    private List<FractalArea> definedAreas = new List<FractalArea>();
    private float areaSize;
    public int areasCount { get; private set; }
    public int definedAreasCount { get; private set; }

    public FractalBuilder(int length, int width, float areaSize)
    {
        this.length = length;
        this.width = width;
        this.areaSize = areaSize;

        GenerateAreas();
        GetCentralArea().Define();
    }

    private void GenerateAreas()
    {
        areas = new FractalArea[length, width];
        areasCount = length * width;
        
        for (int i=0; i<areas.GetLength(0); i++)
        {
            for (int j=0; j<areas.GetLength(1); j++)
            {
                areas[i, j] = new FractalArea(new Vector2(i, j), this);
            }
        }
    }

    private FractalArea GetCentralArea()
    {
        return areas[length / 2, width / 2];
    }

    public List<FractalArea> GetAvailableAreas(Vector2 coords)
    {
        List<FractalArea> availableAreas = new List<FractalArea>();

        AddArea(coords + Vector2.up, availableAreas);
        AddArea(coords + Vector2.down, availableAreas);
        AddArea(coords + Vector2.left, availableAreas);
        AddArea(coords + Vector2.right, availableAreas);

        return availableAreas;
    }

    private void AddArea(Vector2 coords, List<FractalArea> availableAreas)
    {
        int x = (int)coords.x;
        int y = (int)coords.y;

        if (AreaExist(x, y))
        {
            FractalArea area = areas[x, y];
            AddAreaIfAvailable(area, availableAreas);
        }
    }

    private bool AreaExist(int x, int y)
    {
        return (x >= 0 && x < areas.GetLength(0)) && (y >= 0 && y < areas.GetLength(1));
    }

    private void AddAreaIfAvailable(FractalArea area, List<FractalArea> availableAreas)
    {
        if (area.index == 0)
            availableAreas.Add(area);
    }

    public void AreaDefined(FractalArea area)
    {
        definedAreasCount++;
        definedAreas.Add(area);

        if (definedAreasCount >= areasCount)
            RenderAllAreas();
    }

    private void RenderAllAreas()
    {
        foreach (FractalArea area in definedAreas)
            RenderArea(area);
    }

    private void RenderArea(FractalArea area)
    {
        float lengthAdjustment = width / (float)length;
        float widthAdjustment = length / (float)width;
        
        float areaLength = (length > width) ? (areaSize * lengthAdjustment) : (areaSize);
        float areaWidth = (length < width) ? (areaSize * widthAdjustment) : (areaSize);

        //Debug.Log("Area length " + areaLength);
        //Debug.Log("Area width " + areaWidth);

        area.Render(areaLength, areaWidth, 0.9f);
    }

    public void ComeIn(IFractalVisitor visitor)
    {
        foreach (IGuest guest in definedAreas)
            guest.MeetVisitor(visitor);
    }
}
