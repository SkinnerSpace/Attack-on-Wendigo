using System;
using System.Collections.Generic;
using UnityEngine;

public class MainInputReader : MonoBehaviour, IInputReader
{
    private List<InputReader> inputReaders = new List<InputReader>();

    public void Add(InputReader inputReader) => inputReaders.Add(inputReader);

    public T Get<T>() where T : InputReader
    {
        foreach (InputReader reader in inputReaders)
        {
            if (reader is T) return reader as T;
        }

        return null;
    }
}
