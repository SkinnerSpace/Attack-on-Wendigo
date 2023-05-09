using UnityEngine;

public class Domino : MonoBehaviour
{
    [SerializeField] private CollapseController collapseController;

    private Collider dominoCollider;
    private Vector3 collapseDirection;

    private void Awake()
    {
        dominoCollider = GetComponent<Collider>();
        collapseController.onPushDirectionIsSet += ActivateCollider;
    }

    private void ActivateCollider(Vector3 collapseDirection)
    {
        this.collapseDirection = collapseDirection;
        dominoCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        ICollapsible collapsible = other.GetComponent<ICollapsible>();

        if (collapsible != null)
        {
            collapsible.PullDown(collapseDirection);
        }
    }
}
