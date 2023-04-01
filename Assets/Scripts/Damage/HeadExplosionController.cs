using System.Collections.Generic;
using UnityEngine;

public class HeadExplosionController : MonoBehaviour
{
    [Header("Limbs")]
    [SerializeField] private Limb head;
    [SerializeField] private Limb leftHorn;
    [SerializeField] private Limb rightHorn;

    [Header("Particles")]
    [SerializeField] private ParticleSystem tornLeftHorn;
    [SerializeField] private ParticleSystem tornRightHorn;

    [SerializeField] private List<MeshRenderer> meshesToHide; 

    private void Awake(){
        head.SubscribeOnAmputation(TriggerExplosion);
    }

    private void TriggerExplosion()
    {
        if (!leftHorn.IsDestroyed()){
            leftHorn.SwitchOffHitBoxes();
            tornLeftHorn.Play();
        }

        if (!rightHorn.IsDestroyed()){
            rightHorn.SwitchOffHitBoxes();
            tornRightHorn.Play();
        }

        foreach (MeshRenderer meshToHide in meshesToHide){
            meshToHide.enabled = false;
        }
    }
}
