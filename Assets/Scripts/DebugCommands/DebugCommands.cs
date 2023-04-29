using System.Collections.Generic;
using UnityEngine;
using Character;

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

        DebugCommand<int> SPAWN_N = new DebugCommand<int>("spawn", "Spawn a chosen number of Wendigos", "spawn <count>", (n) => 
        {
            components.spawner.Spawn(n);
        });

        DebugCommand HELP_OPEN = new DebugCommand("help", "Show the list of commands", "help", () => {
            components.debugController.showHelp = true;
        });

        DebugCommand HELP_CLOSE = new DebugCommand("help close", "Close the list of commands", "help close", () => {
            components.debugController.showHelp = false;
        });

        DebugCommand DIE = new DebugCommand("die", "Commit suicide", "die", () =>
        {
            components.player.GetController<CharacterHealthSystem>().Die();
        });

        DebugCommand GET_BLUNT_DAMAGE = new DebugCommand("hit", "Get blunt damage", "hit", () =>
        {
            GameEvents.current.PlayerReceivedBluntDamage();
        });

        DebugCommand DECLARE_VICTORY = new DebugCommand("victory", "Declare victory", "victory", () => 
        {
            GameEvents.current.DeclareVictory();
        });

        DebugCommand<float> SET_DEATH_PROGRESS = new DebugCommand<float>("progress", "Set death progress", "progress <progress>", (n) => 
        {
            GameEvents.current.UpdateDeathProgress(n);
        });

        commandList = new List<object>()
        {
            SPAWN,
            SPAWN_N,
            HELP_OPEN,
            HELP_CLOSE,
            DIE,
            GET_BLUNT_DAMAGE,
            DECLARE_VICTORY,
            SET_DEATH_PROGRESS
        };
    }
}