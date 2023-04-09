using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LimbsManager : MonoBehaviour
{
    [SerializeField] private Transform root;
    [SerializeField] private Transform healthSystempImp;

    [Header("Parts")]
    public LimbGroup[] limbGroups;
    public Limb[] limbs;

    [SerializeField] private GoreSFXData goreSFXData;

    private IHealthSystem healthSystem;

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
        foreach (Limb limb in limbs){
            limb.SetSFXPlayer(goreSFXData);
        }
    }

    private void Start()
    {
        healthSystem = healthSystempImp.GetComponent<IHealthSystem>();

        if (healthSystem != null){
            healthSystem.SubscribeOnDeath(MakeLimbsDestroyable);
        }
    }

    public void MakeLimbsDestroyable(){
        Debug.Log("Make destroyable");

        foreach (Limb limb in limbs){
            limb.MakeDestroyable();
        }
    }

    public void ResetState(){
        foreach (Limb limb in limbs){
            limb.ResetState();
        }
    }
}
