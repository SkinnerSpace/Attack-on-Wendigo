using UnityEngine;

[ExecuteAlways]
public class PropDestroyerManager : MonoBehaviour
{
    [SerializeField] private Transform destroyersRoot;
    [SerializeField] private Transform pivot;

    public PropDestroyer[] destroyers;

    private void OnEnable()
    {
        FindDestroyers();
        SetPivotToDestroyers();
    }

    private void FindDestroyers()
    {
        if (destroyersRoot != null)
            destroyers = destroyersRoot.GetComponentsInChildren<PropDestroyer>();
    }

    private void SetPivotToDestroyers()
    {
        if (pivot != null)
        {
            foreach (PropDestroyer destroyer in destroyers)
                destroyer.SetPivot(pivot);
        }
    }
}