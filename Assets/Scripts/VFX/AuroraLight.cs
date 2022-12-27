using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuroraLight : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<MeshFilter>().sharedMesh.RecalculateBounds();
        Debug.Log("Recalculated");
    }
}
