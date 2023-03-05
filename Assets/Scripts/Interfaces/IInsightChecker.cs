using UnityEngine;

public interface IInsightChecker
{
    bool TargetIsVisibleFromPointOfView(Transform target);
}