using System.Collections.Generic;
using UnityEngine;

public class DispenserStorage : MonoBehaviour
{
    private Stack<GameObject> cargo = new Stack<GameObject>();

    public GameObject GetAnItem() => cargo.Pop();
    public void AddAnItem(GameObject item) => cargo.Push(item);

    public bool IsEmpty() => cargo.Count <= 0;
}
