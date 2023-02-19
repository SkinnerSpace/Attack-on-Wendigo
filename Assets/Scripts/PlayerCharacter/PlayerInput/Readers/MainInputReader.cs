using System;
using System.Collections.Generic;
using UnityEngine;

public static class MainInputReader
{
    private static List<InputReader> inputReaders = new List<InputReader>();

    public static void Add(InputReader inputReader) => inputReaders.Add(inputReader);

    public static T Get<T>() where T : InputReader
    {
        foreach (InputReader reader in inputReaders)
        {
            if (reader is T) return reader as T;
        }

        return null;
    }
}
