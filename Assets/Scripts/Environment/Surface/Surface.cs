using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour, ISurface
{
    private const int DEFAULT = 0;

    [SerializeField] private List<SurfaceData> surfaceData;
    private SurfaceData data;

    private ParticleSystem particle;
    private AudioPlayer audioPlayer;
    private SurfaceHitBuilder hitBuilder;

    private IObjectPooler pooler;

    private void Start() => pooler = PoolHolder.Instance;

    public void Set(List<SurfaceData> surfaceData) => this.surfaceData = surfaceData;

    public ISurfaceHitBuilder Hit()
    {
        data = surfaceData[DEFAULT];
        SetUpHitBuilder();

        return hitBuilder;
    }

    public ISurfaceHitBuilder Hit(string particleName)
    {
        if (NotFound(particleName)){
            data = FindData(particleName);
        }
        SetUpHitBuilder();

        return hitBuilder;
    }

    private bool NotFound(string particleName){
        return data == null || 
               data.name != particleName;
    }

    private SurfaceData FindData(string particleName)
    {
        foreach (SurfaceData data in surfaceData){
            if (data.name == particleName)
                return data;
        }

        //Debug.LogError(particleName + " not found");
        return surfaceData[DEFAULT];
    }

    private void SetUpHitBuilder()
    {
        particle = pooler.SpawnFromThePool(data.Name).transform.GetComponent<ParticleSystem>();

        audioPlayer = AudioPlayer.Create(data.sfx.reference).
                                  WithVariety(data.sfx.variety).
                                  WithPitch(data.sfx.minPitch, data.sfx.maxPitch);

        if (hitBuilder == null)
            hitBuilder = new SurfaceHitBuilder();

        hitBuilder.SetUp(particle, audioPlayer);
    }
}
