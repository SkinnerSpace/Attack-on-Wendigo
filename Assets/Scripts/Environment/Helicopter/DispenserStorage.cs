using System.Collections.Generic;
using UnityEngine;

public class DispenserStorage : MonoBehaviour
{
    private Stack<string> cargo = new Stack<string>();

    public string GetAnItem() => cargo.Pop();

    public void AddAnItem(string itemName) => cargo.Push(itemName);

    public bool IsEmpty() => cargo.Count <= 0;

    public int GetCount() => cargo.Count;
}
