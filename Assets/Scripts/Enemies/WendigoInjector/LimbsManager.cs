using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LimbsManager : MonoBehaviour
{
    [SerializeField] private Transform root;
    public LimbGroup[] limbGroups;
    public Limb[] limbs;

    [SerializeField] private GoreSFXData goreSFXData;

    private void OnEnable()
    {
        if (root != null)
        {
            limbGroups = root.GetComponentsInChildren<LimbGroup>();
            limbs = root.GetComponentsInChildren<Limb>();
        }
    }

    private void Awake()
    {
        foreach (Limb limb in limbs)
            limb.SetSFXPlayer(goreSFXData);
    }
}
