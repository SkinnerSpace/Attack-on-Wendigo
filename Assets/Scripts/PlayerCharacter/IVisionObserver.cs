using UnityEngine;
using System.Collections;

public interface IVisionObserver
{
    void OnTargetUpdate(VisionTarget target);
}
