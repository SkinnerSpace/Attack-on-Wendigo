using UnityEngine;

public class Openable : MonoBehaviour, IOpenable
{
    [SerializeField] private Crate crate;
    private SphereCollider sphereCollider;

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    public void Open()
    {
        sphereCollider.enabled = false;
        crate.Open();
    }

    public void ActivateCollision() => sphereCollider.enabled = true;
}
