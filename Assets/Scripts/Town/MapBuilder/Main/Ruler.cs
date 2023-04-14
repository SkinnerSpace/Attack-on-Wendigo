public static class Ruler
{
    public static int GetOffset(PropTypes type, Map map)
    {
        switch (type)
        {
            case PropTypes.BUILDING:
                return (map.size - map.explication.townSize) / 2;

            case PropTypes.TREE:
                return (map.size - map.explication.forestSize) / 2;

            default:
                return 0;
        }
    }
}
