using UnityEngine;

public class SurfaceExplosion : MonoBehaviour, IPooledObject
{
    public string PoolTag { get; set; }
    public GameObject Object => gameObject;
    private ParticleSystem particle;
    private IObjectPooler pooler;

    private void Awake() => particle = GetComponent<ParticleSystem>();

    private void Start() => pooler = PoolHolder.Instance;
    
    public void OnObjectSpawn(){
        particle.Play();
    }

    public void SetActive(bool active) => gameObject.SetActive(active);

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation) { }

    private void OnParticleSystemStopped()
    {
        BackToPool();
    }

    public void BackToPool()
    {
        gameObject.SetActive(false);
        pooler.PutIntoThePool(this);
    }
}
