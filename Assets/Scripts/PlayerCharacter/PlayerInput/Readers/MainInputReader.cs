using System.Collections.Generic;
using UnityEngine;

public class MainInputReader : MonoBehaviour
{
    private InputReader[] inputReaders;

    private void Awake() => FindInputReaders();

    private void FindInputReaders() => inputReaders = GetComponents<InputReader>(); 

    public T GetInputReader<T>() where T : InputReader
    {
        foreach (InputReader reader in inputReaders)
        {
            if (reader is T) return reader as T;
        }

        return null;
    }
}
