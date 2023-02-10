using UnityEngine;

public class BulletExplosion : MonoBehaviour, IPooledObject
{
    public GameObject Object => gameObject;
    private ParticleSystem particle;

    private void Awake() => particle = GetComponent<ParticleSystem>();

    public void OnObjectSpawn() => particle.Play();

    public void SetActive(bool active) => gameObject.SetActive(active);

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation) => transform.SetPositionAndRotation(position, rotation);
}