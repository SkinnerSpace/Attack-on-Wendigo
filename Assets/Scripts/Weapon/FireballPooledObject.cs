using UnityEngine;

public class FireballPooledObject : MonoBehaviour, IPooledObject
{
    public string PoolTag { get; set; }
    public GameObject Object => gameObject;
    private IObjectPooler pooler;

    [SerializeField] private FireballData data;
    [SerializeField] private Fireball fireball;
    [SerializeField] private FireballSFXPlayer sFXPlayer;

    private void Awake() => fireball.Subscribe(BackToPool);

    private void Start() => pooler = PoolHolder.Instance;

    public void OnObjectSpawn()
    {
        data.IsActive = true;
        sFXPlayer.PlayThrowSFX();
        sFXPlayer.PlayFlySFX();
    }

    public void SetActive(bool active) => gameObject.SetActive(active);

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation) => transform.SetPositionAndRotation(position, rotation);

    private void BackToPool()
    {
        sFXPlayer.StopFlySFX();
        gameObject.SetActive(false);
        pooler.PutIntoThePool(this);
    }
}
