using UnityEngine;

public interface ISurfaceHitBuilder
{
    ISurfaceHitBuilder WithPosition(Vector3 position);
    ISurfaceHitBuilder WithAngle(Vector3 direction, Vector3 normal);
    ISurfaceHitBuilder WithShape(float radius, float angle);
    ISurfaceHitBuilder WithScale(float scale);
    ISurfaceHitBuilder WithCount(short minCount, short maxCount);
    void Launch();
}
