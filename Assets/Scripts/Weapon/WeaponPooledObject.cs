using UnityEngine;

public class WeaponPooledObject : MonoBehaviour, IPooledObject
{
    public GameObject Object => gameObject;
    [SerializeField] private Pickable pickable;

    public void OnObjectSpawn()
    {
        float randomAngle = Rand.Range(0f, 360f);
        Vector3 randomEuler = new Vector3(0f, randomAngle, 0f);
        Quaternion randomRotation = Quaternion.Euler(randomEuler);
        transform.rotation = randomRotation;

        pickable.ResetState();
    }

    public void SetActive(bool active) => gameObject.SetActive(active);

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation) => transform.SetPositionAndRotation(position, rotation);
}

