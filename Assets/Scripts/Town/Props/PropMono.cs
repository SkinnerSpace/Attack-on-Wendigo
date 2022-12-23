using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMono : MonoBehaviour
{
    [SerializeField] private PropSetup propSetup;
    private Prop prop;

    private void Awake()
    {
        PropConfigurator.Configure(propSetup, new TransformProxy(transform));
        prop = PropAssembly.CreateProp(propSetup, transform);

        prop.AddToTown();
    }
}
