using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour, ISurface
{
    [SerializeField] private string particleName;
    [SerializeField] private SurfaceHitSFXData sFXData;

    private AudioPlayer audioPlayer;
    private IObjectPooler pooler;

    private void Start() => pooler = PoolHolder.Instance;

    public void Set(string particleName, SurfaceHitSFXData sFXData){
        this.particleName = particleName;
        this.sFXData = sFXData;
    }

    private void Awake(){
        audioPlayer = AudioPlayer.Create(sFXData.reference).WithVariety(sFXData.variety).WithPitch(sFXData.minPitch, sFXData.maxPitch);
    }

    public ISurfaceHitBuilder Hit()
    {
        ParticleSystem particle = pooler.SpawnFromThePool(particleName).transform.GetComponent<ParticleSystem>();
        SurfaceHitBuilder hitBuilder = new SurfaceHitBuilder(particle, audioPlayer);

        return hitBuilder;
    }
}
