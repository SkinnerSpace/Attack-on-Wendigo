using System.Collections.Generic;
using UnityEngine;

public class FractalArea : IGuest
{
    public int depth { get; private set; }
    public int index { get; private set; }
    public Vector2 coords { get; private set; }

    private Vector3 position = Vector3.zero;
    private Vector3 size = Vector3.zero;

    private List<FractalArea> availableAreas = new List<FractalArea>();
    private List<FractalArea> areasUnderConstruction = new List<FractalArea>();

    private FractalMediator mediator;
    public FractalData data;

    private FractalBuilder fractalBuilder;

    public FractalArea(AreaBootstrap bootstrap)
    {
        depth = bootstrap.depth;
        coords = bootstrap.coords;
        mediator = bootstrap.mediator;
    }

    public void Define()
    {
        index = mediator.AskForIndex(this);
        KeepGrowing();
    }

    private void KeepGrowing()
    {
        availableAreas = mediator.GetAvailableAreas(coords);

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
        GoDeeper();
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

        data.depth = depth;
        data.index = index;
        data.position = position;
        data.size = size;
    }

    private void GoDeeper()
    {
        if (depth > 0)
        {
            
        }
    }

    public void MeetVisitor(IFractalVisitor visitor)
    {
        visitor.GatherFractalData(data);
    }
}
