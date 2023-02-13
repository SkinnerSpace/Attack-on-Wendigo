using UnityEngine;

public interface IGroundDetector
{
    bool Check(Vector3 position, float radius);
}
