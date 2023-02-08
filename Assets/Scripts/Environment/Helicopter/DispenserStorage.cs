using System.Collections.Generic;
using UnityEngine;

public class DispenserStorage : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapon = new List<GameObject>();
    private List<IPickable> items = new List<IPickable>();

    public bool hasItems = true;

    public GameObject GetAnItem()
    {
        int randomIndex = Rand.Range(0, weapon.Count);
        return weapon[randomIndex];
    }
}
