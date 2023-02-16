using UnityEngine;
using System.Collections;

public interface IVisionObserver
{
    void OnUpdate(VisionTarget target);
}
