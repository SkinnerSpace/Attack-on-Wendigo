using UnityEngine;

public abstract class InputReader : MonoBehaviour
{
    [SerializeField] protected UniversalInputReader input;
    [SerializeField] protected KeyBinds keys;
}

