using System;
using UnityEngine;

public class Architect : MonoBehaviour, IArchitect
{
    private const float BUILDING_CHANCE = 0.05f;
    private int CentralCell => mapSize / 2;

    [SerializeField] public bool debugMode;

    [SerializeField] public int mapSize;
    [SerializeField] public int forestSize;
    [SerializeField] public int townSize;
    [SerializeField] public float scale;
    public Vector3 center { get; set; }

    private DesignDepartment designDepartment;
    [SerializeField] private Builder builder;
    [SerializeField] private AirDispatcher airDispatcher;

    [SerializeField] private CentralizedObjects centralizedObjects;

    private Map map;

    public void BuildTheTown()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);

        if (designDepartment == null){
            designDepartment = new DesignDepartment();
        }

        GenerateMap(); // GIVE MAP TO THE AIR DISPATCHER!!!
        FindCenter();

        centralizedObjects.Centralize(center);

        SetMarks();

        Blueprint blueprint = DrawBlueprint();
        builder.Build(blueprint);
    }

    private void SetMarks()
    {
        AddMarks(new Mark(PropTypes.BUILDING, 1), likelyhood: BUILDING_CHANCE);
        AddMarks(new Mark(PropTypes.BUILDING, 2), likelyhood: BUILDING_CHANCE);
        AddMarks(new Mark(PropTypes.BUILDING, 3), likelyhood: BUILDING_CHANCE);
        AddMarks(new Mark(PropTypes.BUILDING, 4), likelyhood: BUILDING_CHANCE);
        AddMarks(new Mark(PropTypes.TREE, 1), likelyhood: 0.4f);
    }

    private void GenerateMap()
    {
        map = new Map(mapSize);

        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                Mark mark = new Mark(PropTypes.NONE, Index: 0);
                map.SetMark(x, y, mark);
            }
        }
    }

    private void FindCenter()
    {
        float totalSize = mapSize * scale;
        center = new Vector3(totalSize / 2, 0, totalSize / 2);

    }

    public void AddMarks(Mark mark, float likelyhood)
    {
        int offset = GetOffset(mark.Type);

        for (int y = offset; y < mapSize - offset; y++)
        {
            for (int x = offset; x < mapSize - offset; x++)
            {
                if (map.IsEmpty(x, y)){
                    Requirments doc = new Requirments(map, new Cell(x, y), mark, likelyhood);
                    Mark newMark = designDepartment.Design(doc);
                    map.SetMark(x, y, newMark);
                }
            }
        }

        Mark emptyMark = new Mark(PropTypes.NONE, Index: 0);
        map.SetMark(CentralCell, CentralCell, emptyMark);
    }

    private Blueprint DrawBlueprint()
    {
        int offset = GetOffset(PropTypes.TREE);
        Blueprint blueprint = new Blueprint(map, offset, scale, transform);
        return blueprint;
    }

    public int GetOffset(PropTypes type)
    {
        switch (type)
        {
            case PropTypes.BUILDING:
                return (mapSize - townSize) / 2;

            case PropTypes.TREE:
                return (mapSize - forestSize) / 2;

            default:
                return 0;
        }
    }

    private void CutMark()
    {

    }

    # if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!debugMode) return;

        FindCenter();

        float forestFullSize = (forestSize * scale);
        Vector3 forestCubeSize = new Vector3(forestFullSize, 20f, forestFullSize);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, forestCubeSize);

        float townFullSize = (townSize * scale);
        Vector3 townCubeSize = new Vector3(townFullSize, 20f, townFullSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(center, townCubeSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, (forestFullSize / 2) * 0.8f);

        float mapFullSize = (mapSize * scale);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(center, mapFullSize / 2);
    }
    #endif
}
