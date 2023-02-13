using System.Collections;
using UnityEngine;

public abstract class InputReader : MonoBehaviour
{
    protected UniversalInputReader input;
    protected KeyBinds keys;

    private void Awake()
    {
        input = GetComponent<UniversalInputReader>();
        keys = GetComponent<KeyBinds>();
    }
}
