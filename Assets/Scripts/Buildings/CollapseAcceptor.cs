using UnityEngine;

public class CollapseAcceptor : MonoBehaviour
{
    public MeshRenderer mesh { get; private set; }

    public Vector3 originalPos { get; private set; }
    private Vector3 collapsePos;

    public Quaternion originalRot { get; private set; }
    private Quaternion collapseRot;


    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();

        originalPos = collapsePos = transform.position;
        originalRot = collapseRot = transform.rotation;
    }

    public CollapseAcceptor Add(Vector3 displacement)
    {
        collapsePos += displacement;
        return this;
    }

    public CollapseAcceptor Add(Quaternion collapseRot)
    {
        this.collapseRot = collapseRot;
        return this;
    }

    public void Apply()
    {
        transform.position = collapsePos;
        collapsePos = originalPos;

        transform.rotation = collapseRot;
        collapseRot = originalRot;
    }

    public void Disappear() => mesh.enabled = false;
}
