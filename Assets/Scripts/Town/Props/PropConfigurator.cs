using System.Collections.Generic;
using UnityEngine;

public static class PropConfigurator
{
    public static void Configure(PropSetup propSetup, ITransformProxy transform)
    {
        SetRandomScale(transform, propSetup.minScale, propSetup.maxScale);
        SetRandomAngle(transform, propSetup.angles);
    }

    public static void SetRandomScale(ITransformProxy transform, float minScale, float maxScale)
    {
        float scale = UnityEngine.Random.Range(minScale, maxScale);
        transform.LocalScale = new Vector3(
            transform.LocalScale.x * scale, 
            transform.LocalScale.y * scale, 
            transform.LocalScale.z * scale);
    }

    public static void SetRandomAngle(ITransformProxy transform, List<float> angles)
    {
        if (angles.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, angles.Count);
            transform.Angle = new Vector3(0f, angles[index], 0f);
        }
        else
        {
            transform.Angle = new Vector3(0f, UnityEngine.Random.Range(0f, 360f), 0f);
        }
    }
}