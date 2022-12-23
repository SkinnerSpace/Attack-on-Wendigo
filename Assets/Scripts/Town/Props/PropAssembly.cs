using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class PropAssembly
{
    public static Prop CreateProp(PropSetup setup, Transform transform)
    {
        IPropData data = CreateData(setup, transform);
        ITransformProxy transformProxy = new TransformProxy(transform);

        Prop prop = PropFactory.CreateProp(setup.type);
        prop.SetData(data);
        prop.SetTransform(transformProxy);

        return prop;
    }

    public static IPropData CreateData(PropSetup setup, Transform transform)
    {
        PropData data = new PropData(setup);

        Vector3 size = GetSize(transform);
        data.SetSize(size);

        return data;
    }

    public static Vector3 GetSize(Transform transform)
    {
        Collider collider = transform.GetComponentInChildren<Collider>();

        Vector3 size = collider.bounds.size;
        Vector3 scale = transform.localScale;

        Vector3 scaledSize = new Vector3(
            size.x * scale.x, 
            size.y * scale.y, 
            size.z * scale.z);

        return scaledSize;
    }
}
