using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LimbsManager : MonoBehaviour
{
    [SerializeField] private Transform root;
    public LimbGroup[] limbGroups;
    public Limb[] limbs;

    private void OnEnable()
    {
        if (root != null)
        {
            limbGroups = root.GetComponentsInChildren<LimbGroup>();
            limbs = root.GetComponentsInChildren<Limb>();
        }
    }
}

