using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SharedSurfaceData", menuName ="ScriptableObjects/SharedSurfaceData")]
public class SharedSurfaceData : ScriptableObject
{
    public List<SurfaceData> data;
}