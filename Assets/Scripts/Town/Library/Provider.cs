using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Provider : MonoBehaviour
{
    [SerializeField] private List<GameObject> buildings = new List<GameObject>();
    [SerializeField] private List<GameObject> trees = new List<GameObject>();

    private Dictionary<Types, List<GameObject>> objects = new Dictionary<Types, List<GameObject>>();

    private void Awake()
    {
        InitializeObjects();
    }

    private void InitializeObjects()
    {
        objects.Add(Types.BUILDING, buildings);
        objects.Add(Types.TREE, trees);
    }

    public GameObject GetObject(Mark mark)
    {
        return objects[mark.type][mark.index-1];
    }
}
