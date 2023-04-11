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

    [Header("Sounds")]
    [SerializeField] private ParticleSoundSystem leftHornSFX;
    [SerializeField] private ParticleSoundSystem rightHornSFX;

    [SerializeField] private List<MeshRenderer> meshesToHide; 

    private void Awake(){
        head.SubscribeOnAmputation(TriggerExplosion);
    }

    private void TriggerExplosion()
    {
        if (!leftHorn.IsDestroyed()){
            leftHorn.SwitchOffHitBoxes();
            tornLeftHorn.Play();
            leftHornSFX.SwitchOn();
        }

        if (!rightHorn.IsDestroyed()){
            rightHorn.SwitchOffHitBoxes();
            tornRightHorn.Play();
            rightHornSFX.SwitchOn();
        }

        foreach (MeshRenderer meshToHide in meshesToHide){
            meshToHide.enabled = false;
        }
    }
}
