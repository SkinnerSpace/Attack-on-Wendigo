using System;
using UnityEngine;

public class Architect : MonoBehaviour, IArchitect
{
    [SerializeField] public bool debugMode;
    [SerializeField] public MapExplication explication;
    public Vector3 center { get; set; }

    private Cartographer cartographer;
    [SerializeField] private Builder builder;
    [SerializeField] private AirDispatcher airDispatcher;

    [SerializeField] private CentralizedObjects centralizedObjects;

    public void BuildTheTown()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);

        if (cartographer == null){
            cartographer = new Cartographer(transform);
        }

        Map map = cartographer.GenerateMap(explication); 
        airDispatcher.SetMap(map);

        FindCenter();
        centralizedObjects.Centralize(center);

        Blueprint blueprint = DrawBlueprint(map);
        builder.Build(blueprint);
    }

    private void FindCenter(){
        float totalSize = explication.size * explication.scale;
        center = new Vector3(totalSize / 2, 0, totalSize / 2);
    }

    private Blueprint DrawBlueprint(Map map)
    {
        int offset = Ruler.GetOffset(PropTypes.TREE, map);
        Blueprint blueprint = new Blueprint(map, offset, explication.scale, transform);
        return blueprint;
    }

    # if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!debugMode) return;

        FindCenter();

        float forestFullSize = (explication.forestSize * explication.scale);
        Vector3 forestCubeSize = new Vector3(forestFullSize, 20f, forestFullSize);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, forestCubeSize);

        float townFullSize = (explication.townSize * explication.scale);
        Vector3 townCubeSize = new Vector3(townFullSize, 20f, townFullSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(center, townCubeSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, (forestFullSize / 2) * 0.8f);

        float mapFullSize = (explication.size * explication.scale);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(center, mapFullSize / 2);
    }
    #endif
}
