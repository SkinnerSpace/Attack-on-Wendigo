using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMono : MonoBehaviour, IDestructible
{
    [SerializeField] private PropSetup propSetup;
    [SerializeField] private Transform model;
    private Prop prop;
    private Collider hitBox;

    private void Awake()
    {
        PropConfigurator.Configure(propSetup, new TransformProxy(transform));
        prop = PropAssembly.CreateProp(propSetup, transform);
        hitBox = GetComponent<Collider>();

        prop.AddToTown();
    }

    public void Collapse(Vector3 impact)
    {
        hitBox.enabled = false;
        prop.Collapse(impact);
        StartCoroutine(PlayCollapseAnimation());
    }

    IEnumerator PlayCollapseAnimation()
    {
        while (prop.isCollapsing)
        {
            prop.UpdateCollapse();
            yield return null;
        }
    }
}
