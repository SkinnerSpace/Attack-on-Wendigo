using System;
using UnityEngine;

public class BloodSplatterPooledObject : MonoBehaviour, IPooledObject
{
    public string PoolTag { get; set; }
    public GameObject Object => gameObject;

    private BloodSplatter bloodSplatter;
    private IObjectPooler pooler;

    private void Awake(){
        bloodSplatter = GetComponent<BloodSplatter>();
        bloodSplatter.onStopBleeding += BackToPool;
    }

    private void Start()
    {
        pooler = PoolHolder.Instance;
    }

    public void OnObjectSpawn()
    {
        bloodSplatter.Initialize();
    }

    public void SetActive(bool active)
    {
/*        if (active){
            bloodSplatter.Launch();
        }
*/
        gameObject.SetActive(active);
    }

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation) { }

    public void BackToPool()
    {
        bloodSplatter.ResetState();
        gameObject.SetActive(false);
        pooler.PutIntoThePool(this);
    }

    public void SubscribeOnSpawn(Action onSpawn) { }
}