public class Cartographer
{
    private DesignDepartment designDepartment;

    public Cartographer(){
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
            Mark mark = new Mark(data.propType, data.propIndex);
            AddMarks(map, mark, data.likelyhood);
        }
    }

    public void AddMarks(Map map, Mark mark, float likelyhood)
    {
        int offset = Ruler.GetOffset(mark.Type, map);

        for (int y = offset; y < map.size - offset; y++)
        {
            for (int x = offset; x < map.size - offset; x++)
            {
                AddMarkOnTheMapAtPositionWithALikelyhood(mark, map, x, y, likelyhood);
            }
        }

        SetEmptyMark(map, map.Center);
    }

    private void AddMarkOnTheMapAtPositionWithALikelyhood(Mark mark, Map map, int x, int y, float likelyhood)
    {
        if (map.IsEmpty(x, y))
        {
            Requirments doc = new Requirments(map, new Cell(x, y), mark, likelyhood);
            Mark newMark = designDepartment.Design(doc);
            map.SetMark(x, y, newMark);
        }
    }

    private void SetEmptyMark(Map map, int x, int y)
    {
        Mark mark = new Mark(PropTypes.NONE, Index: 0);
        map.SetMark(x, y, mark);
    }

    private void SetEmptyMark(Map map, Cell cell)
    {
        Mark mark = new Mark(PropTypes.NONE, Index: 0);
        map.SetMark(cell, mark);
    }
}
