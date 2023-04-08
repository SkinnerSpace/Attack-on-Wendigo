using System.Collections.Generic;
using UnityEngine;

public class Provider : MonoBehaviour
{
    [SerializeField] private List<GameObject> buildings = new List<GameObject>();
    [SerializeField] private List<GameObject> trees = new List<GameObject>();

    private readonly Dictionary<PropTypes, List<GameObject>> objects = new Dictionary<PropTypes, List<GameObject>>();

    private bool isInitialized = false;

    public GameObject GetObject(Mark mark)
    {
        if (!isInitialized){
            InitializeObjects();
        }

        return objects[mark.Type][mark.Index-1];
    }

    private void InitializeObjects()
    {
        objects.Add(PropTypes.BUILDING, buildings);
        objects.Add(PropTypes.TREE, trees);

        isInitialized = true;
    }

}
