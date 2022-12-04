using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IBuilding
{
    public enum Type
    {
        House,
        BlockOfFlats
    }

    [SerializeField] private Type type;

    private Town town;

    public void SetTown(Town town)
    {
        this.town = town;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public string GetName()
    {
        return transform.name;
    }
}
