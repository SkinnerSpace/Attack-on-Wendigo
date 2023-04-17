using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCommandBase
{
    private string commandId;
    private string commandDescription;
    private string commandFormat;

    public string CommandId => commandId;
    public string CommandDescription => commandDescription;
    public string CommandFormat => commandFormat;

    public DebugCommandBase(string id, string description, string format)
    {
        commandId = id;
        commandDescription = description;
        commandFormat = format;
    }
}
