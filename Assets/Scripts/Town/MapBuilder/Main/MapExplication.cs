using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MapExplication", menuName ="ScriptableObjects/MapExplication")]
public class MapExplication : ScriptableObject
{
    public int size;
    public int forestSize;
    public int townSize;
    public float scale;

    public List<MapMarkData> marks;
}
