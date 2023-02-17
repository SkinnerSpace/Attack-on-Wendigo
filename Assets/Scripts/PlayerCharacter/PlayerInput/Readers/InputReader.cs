using System.Collections;
using UnityEngine;

public abstract class InputReader : MonoBehaviour
{
    protected UnityInputReader input;
    protected KeyBinds keys;

    private void Awake()
    {
        MainInputReader.Add(this);
        input = GetComponent<UnityInputReader>();
        keys = GetComponent<KeyBinds>();
    }
}
