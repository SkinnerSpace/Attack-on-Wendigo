using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FractalArea : IGuest
{
    public int index { get; private set; }
    public Vector2 coords { get; private set; }

    private Vector3 position = Vector3.zero;
    private Vector3 size = Vector3.zero;

    private List<FractalArea> availableAreas = new List<FractalArea>();
    private List<FractalArea> areasUnderConstruction = new List<FractalArea>();

    private FractalBuilder builder;
    public FractalData data;

    public FractalArea(Vector2 coords, FractalBuilder builder)
    {
        this.coords = coords;
        this.builder = builder;
    }

    public void Define()
    {
        builder.AreaDefined(this);
        index = builder.definedAreasCount;
        Debug.Log(index);

        KeepGrowing();
    }

    private void KeepGrowing()
    {
        availableAreas = builder.GetAvailableAreas(coords);

        if (availableAreas.Count > 0)
        {
            OccupyAvailableAreas();
            DefineAdjacentAreas();
        }
    }

    private void OccupyAvailableAreas()
    {
        for (int i = 0; i < availableAreas.Count; i++)
        {
            FractalArea area = availableAreas[i];
            area.Occupy();
            areasUnderConstruction.Add(area);
        }
    }

    public void Occupy()
    {
        index = -1;
    }

    private void DefineAdjacentAreas()
    {
        foreach (FractalArea area in availableAreas)
            area.Define();
    }

    public void Render(float length, float width, float filled)
    {
        Vector2 space = new Vector2(length, width);
        Vector2 filledSpace = space * filled;

        float x = RenderCoord(coords.x, space.x);
        float y = RenderCoord(coords.y, space.y);

        position = new Vector3(x, 0.5f, y);
        size = new Vector3(filledSpace.x, 1f, filledSpace.y) * 0.1f;

        PackData();
    }

    private float RenderCoord(float virtualCoord, float spaceCoord)
    {
        float offset = spaceCoord / 2;
        float renderedCoord = (virtualCoord * spaceCoord) + offset;

        return renderedCoord;
    }

    private void PackData()
    {
        data = new FractalData();

        //data.layer = layer;
        data.index = index;
        data.position = position;
        data.size = size;
    }

    public void MeetVisitor(IFractalVisitor visitor)
    {
        visitor.GatherFractalData(data);
    }
}
