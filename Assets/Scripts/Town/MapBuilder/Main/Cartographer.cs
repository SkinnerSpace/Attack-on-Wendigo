using UnityEngine;

public class Cartographer
{
    private Transform container;
    private DesignDepartment designDepartment;

    public Cartographer(Transform container){
        this.container = container;
        designDepartment = new DesignDepartment();
    }

    public Map GenerateMap(MapExplication explication)
    {
        Map map = GenerateEmptyMap(explication);
        SetMarksOnTheMap(map);

        return map;
    }

    private Map GenerateEmptyMap(MapExplication explication)
    {
        Map map = new Map(explication);

        for (int y = 0; y < explication.size; y++)
        {
            for (int x = 0; x < explication.size; x++)
            {
                SetEmptyMark(map, x, y);
            }
        }

        return map;
    }

    private void SetMarksOnTheMap(Map map)
    {
        foreach (MapMarkData data in map.explication.marks){
            AddMarks(map, data);
        }
    }

    public void AddMarks(Map map, MapMarkData data)
    {
        int offset = Ruler.GetOffset(data.propType, map);

        for (int y = offset; y < map.size - offset; y++)
        {
            for (int x = offset; x < map.size - offset; x++)
            {
                Mark mark = new Mark(data.propType, data.propIndex, x, y, map.explication.scale, container);
                AddMarkOnTheMapAtPositionWithALikelyhood(mark, map, data.likelyhood, data.increaseLikelyhood);
            }
        }

        //SetEmptyMark(map, map.Center);
        SetChosenMark(map, map.Center, PropTypes.BUILDING, 5);
    }

    private void AddMarkOnTheMapAtPositionWithALikelyhood(Mark mark, Map map, float likelyhood, bool increaseLikelyhood)
    {
        if (map.IsEmpty(mark.mapPosition))
        {
            Requirments doc = new Requirments(map, mark.mapPosition, mark, likelyhood, increaseLikelyhood);
            Mark newMark = designDepartment.Design(doc);
            map.SetMark(newMark);
        }
    }

    private void SetEmptyMark(Map map, int x, int y)
    {
        Mark mark = new Mark(PropTypes.NONE, Index: 0, x, y, map.explication.scale, container);
        map.SetMark(x, y, mark);
    }

    private void SetEmptyMark(Map map, Cell cell)
    {
        Mark mark = new Mark(PropTypes.NONE, Index: 0, cell.X, cell.Y, map.explication.scale, container);
        map.SetMark(mark);
    }

    private void SetChosenMark(Map map, Cell cell, PropTypes prop, int index)
    {
        Mark mark = new Mark(prop, index, cell.X, cell.Y, map.explication.scale, container);
        map.SetMark(mark);
    }
}
