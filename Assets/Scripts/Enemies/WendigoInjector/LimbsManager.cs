using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LimbsManager : MonoBehaviour
{
    [SerializeField] private Transform root;
    [SerializeField] private Transform healthSystempImp;
    [SerializeField] private int healthMultiplier;

    [Header("Parts")]
    public LimbGroup[] limbGroups;
    public Limb[] limbs;

    [SerializeField] private GoreSFXData goreSFXData;

    private IHealthSystem healthSystem;

#if UNITY_EDITOR
    private void OnEnable()
    {
        if (root != null)
        {
            limbGroups = root.GetComponentsInChildren<LimbGroup>();
            limbs = root.GetComponentsInChildren<Limb>();

            foreach (Limb limb in limbs){
                limb.SetHealthMultiplier(healthMultiplier);
            }
        }
    }
#endif

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
