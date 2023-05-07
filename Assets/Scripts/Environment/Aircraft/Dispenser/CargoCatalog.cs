using UnityEngine;

[CreateAssetMenu(fileName ="CargoCatalog", menuName ="ScriptableObjects/CargoCatalog")]
public class CargoCatalog : ScriptableObject
{
    public CargoItem[] items;
}