using UnityEngine;


[ExecuteAlways]
public class PropDestroyerManager : MonoBehaviour
{
    [SerializeField] private Transform destroyersRoot;
    [SerializeField] private Transform pivot;

    public PropDestroyer[] destroyers;

#if UNITY_EDITOR
    private void OnEnable()
    {
        FindDestroyers();
    }
#endif

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
