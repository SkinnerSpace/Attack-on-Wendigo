using System.Collections.Generic;
using UnityEngine;

public class WendigoManager
{
    public List<Transform> Wendigos => wendigos;
    private List<Transform> wendigos;

    private IObjectPooler pooler;

    private WendigoSpawnerLogic spawner;
    private Transform container;
    private Transform characterImp;

    private static float scale = 1f;

    public WendigoManager(WendigoSpawnerLogic spawner, Transform container, Transform characterImp, IObjectPooler pooler)
    {
        this.spawner = spawner;
        this.container = container;
        this.characterImp = characterImp;
        this.pooler = pooler;

        wendigos = new List<Transform>();
    }

    public void InstantiateWendigo(Vector3 position, WendigoSpawnerData data)
    {
        Transform wendigoImp = pooler.SpawnFromThePool("Wendigo").transform;
        //wendigoImp.localScale = scale.ToVector3(); // Experiment
        wendigos.Add(wendigoImp);

        IWendigo wendigo = wendigoImp.GetComponent<IWendigo>();
        wendigo.SetHealth(data.health);
        wendigo.SetTarget(characterImp);
        wendigo.SubscribeOnDeath(OnDeath);

        SetInPlace(wendigoImp, position);

        //scale += 0.2f;
    }

    private void SetInPlace(Transform wendigo, Vector3 position)
    {
        wendigo.SetParent(container);
        wendigo.position = position;

        wendigo.LookAt(characterImp.transform);
        wendigo.eulerAngles = new Vector3(0f, wendigo.eulerAngles.y, 0f);
    }

    private void OnDeath(Transform wendigo){
        wendigos.Remove(wendigo);
        spawner.OnDeath();
    }
}
