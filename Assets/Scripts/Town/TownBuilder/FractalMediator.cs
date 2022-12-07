using System.Collections.Generic;
using UnityEngine;

public class FractalMediator
{
    private FractalBuilder builder;

    public FractalMediator(FractalBuilder builder)
    {
        this.builder = builder;
    }

    public List<FractalArea> GetAvailableAreas(Vector2 coords)
    {
        List<FractalArea> availableAreas = new List<FractalArea>();

        LookForArea(coords + Vector2.up, availableAreas);
        LookForArea(coords + Vector2.down, availableAreas);
        LookForArea(coords + Vector2.left, availableAreas);
        LookForArea(coords + Vector2.right, availableAreas);

        return availableAreas;
    }

    private void LookForArea(Vector2 coords, List<FractalArea> availableAreas)
    {
        int x = (int)coords.x;
        int y = (int)coords.y;

        if (AreaExist(x, y))
        {
            FractalArea area = builder.GetArea(x, y);
            AddAreaIfAvailable(area, availableAreas);
        }
    }

    private bool AreaExist(int x, int y)
    {
        return (x >= 0 && x < builder.length) && (y >= 0 && y < builder.width);
    }

    private void AddAreaIfAvailable(FractalArea area, List<FractalArea> availableAreas)
    {
        if (area.index == 0)
            availableAreas.Add(area);
    }

    public int AskForIndex(FractalArea area)
    {
        builder.AreaIsDefined(area);
        return builder.definedAreasCount;
    }
}
