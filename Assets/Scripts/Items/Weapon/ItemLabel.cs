using UnityEngine;

public class ItemLabel : MonoBehaviour
{
    private static int id;
    [SerializeField] private string itemName;

    private void AssignName() => transform.name = itemName + "_" + (++id);
}
