using System.Collections.Generic;
using UnityEngine;

public class AntlersExplosionController : MonoBehaviour
{
    [SerializeField] private HeadExplosionController headExplosionController;

    public List<Limb> limbs;
    public List<ParticleSystem> particles;
    public List<ParticleSoundSystem> soundSystems;
    public List<MeshRenderer> meshesToHide;

    private void Awake()
    {
        headExplosionController.onExplosion += Explode;
    }

    private void Explode()
    {
        for (int i = 0; i < limbs.Count; i++)
        {
            if (!limbs[i].IsDestroyed()){
                limbs[i].SwitchOffHitBoxes();
                particles[i].Play();
                soundSystems[i].SwitchOn();
            }
        }

        foreach (MeshRenderer mesh in meshesToHide)
        {
            mesh.enabled = false;
        }
    }
}
