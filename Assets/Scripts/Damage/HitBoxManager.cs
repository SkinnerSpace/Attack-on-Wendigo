using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class HitBoxManager : MonoBehaviour
{
    [SerializeField] private Transform hitBoxesRoot;

    public HitBoxBehavior[] hits;

    private void OnEnable()
    {
        if (hitBoxesRoot != null) 
            hits = hitBoxesRoot.GetComponentsInChildren<HitBoxBehavior>();
    }
}
