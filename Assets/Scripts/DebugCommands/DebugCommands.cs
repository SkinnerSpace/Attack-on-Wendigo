using System.Collections.Generic;
using UnityEngine;

public class DebugCommands : MonoBehaviour
{
    [SerializeField] private DebugComponents components;
    private List<object> commandList;

    public int Count => commandList.Count;
    public object Get(int index) => commandList[index];

    private void Awake()
    {
        DebugCommand SPAWN = new DebugCommand("spawn", "Spawn a wendigo", "spawn", () => {
            components.spawner.Spawn();
        });

        DebugCommand<int> SPAWN_N = new DebugCommand<int>("spawn", "Spawn a chosen number of Wendigos", "spawn <count>", (n) => {
            components.spawner.Spawn(n);
        });

        DebugCommand HELP_OPEN = new DebugCommand("help", "Show the list of commands", "help", () => {
            components.debugController.showHelp = true;
        });

        DebugCommand HELP_CLOSE = new DebugCommand("help close", "Close the list of commands", "help close", () => {
            components.debugController.showHelp = false;
        });

/*        DebugCommand<bool> SPAWNER_SET_CONFIGURABLE = new DebugCommand<bool>("spawner configurable", "Set spawner configurable", "spawner configurable <yes/no>", (x) => {
            components.spawner.da
        });*/

        commandList = new List<object>()
        {
            SPAWN,
            SPAWN_N,
            HELP_OPEN,
            HELP_CLOSE
        };
    }
}