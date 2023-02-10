using System.Collections.Generic;
using UnityEngine;

public class PoolObjectsNestedContainer
{
    private Transform anchor;
    private static Dictionary<string, Transform> containers = new Dictionary<string, Transform>();

    public PoolObjectsNestedContainer(Transform anchor) => this.anchor = anchor; 

    public Transform GetContainer(string tag) => containers.ContainsKey(tag) ? containers[tag] : CreateContainer(tag);

    private Transform CreateContainer(string tag)
    {
        Transform container = new GameObject(tag).transform;

        container.SetParent(anchor);
        containers.Add(tag, container);

        return container;
    }
}
