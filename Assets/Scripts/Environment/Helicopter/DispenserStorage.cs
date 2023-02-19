using System.Collections.Generic;
using UnityEngine;

public class DispenserStorage : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapon = new List<GameObject>();
    private List<IOldPickable> items = new List<IOldPickable>();

    public bool hasItems = true;

    public GameObject GetAnItem()
    {
        int randomIndex = Rand.Range(0, weapon.Count);
        return weapon[randomIndex];
    }
}
