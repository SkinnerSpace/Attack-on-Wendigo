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
    }

    private void FindDestroyers()
    {
        if (destroyersRoot != null)
            destroyers = destroyersRoot.GetComponentsInChildren<PropDestroyer>();
    }

    public void ResetState()
    {
        foreach (PropDestroyer destroyer in destroyers){
            destroyer.SwitchOn();
        }
    }
}