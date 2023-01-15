using UnityEngine;

public class CollapseAcceptor : MonoBehaviour
{
    private Vector3 originalPos;
    private Vector3 collapsePos;

    private void Awake() => originalPos = collapsePos = transform.position;

    public CollapseAcceptor Add(Vector3 displacement)
    {
        collapsePos += displacement;
        return this;
    }

    public void Apply()
    {
        transform.position = collapsePos;
        collapsePos = originalPos;
    }
}
