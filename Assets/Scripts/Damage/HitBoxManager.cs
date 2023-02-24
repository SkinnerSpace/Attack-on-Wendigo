using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class HitBoxManager : MonoBehaviour
{
    [SerializeField] private Transform hitBoxesRoot;

    public HitBox[] hits;

    private void OnEnable()
    {
        if (hitBoxesRoot != null) 
            hits = hitBoxesRoot.GetComponentsInChildren<HitBox>();
    }
}
