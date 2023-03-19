using UnityEngine;

public class CorpseGroundCollider : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float radius;
    [SerializeField] private bool visualize;

    private Collider[] hitColliders;
    private Vector3 center => transform.position + offset;

    private void Update()
    {
        hitColliders = new Collider[1];

        int collidersCount = Physics.OverlapSphereNonAlloc(center, radius, hitColliders, ComplexLayers.Solid);

/*        for (int i = 0; i < collidersCount; i++)
            Debug.Log(hitColliders[i].transform.name);*/
    }

# if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (visualize)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(center, radius);
            Gizmos.color = Color.white;
        }
    }
# endif
}


