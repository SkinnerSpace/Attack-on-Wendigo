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
            components.player.GetController<CharacterHealthSystem>().Die(DamageTypes.Blunt);
        });

        DebugCommand GET_BLUNT_DAMAGE = new DebugCommand("hit", "Get blunt damage", "hit", () =>
        {
            PlayerEvents.current.NotifyOnDamage();
            PlayerEvents.current.NotifyOnReceivedBluntDamage();
        });

        DebugCommand DECLARE_VICTORY = new DebugCommand("victory", "Declare victory", "victory", () => 
        {
            GameEvents.current.DeclareVictory();
        });

        DebugCommand<float> SET_DEATH_PROGRESS = new DebugCommand<float>("progress", "Set death progress", "progress <progress>", (n) => 
        {
            GameEvents.current.UpdateDeathProgress(n);
        });

        DebugCommand<int, int> ADD_CARGO = new DebugCommand<int, int>("cargo", "Add cargo", "cargo <kind> <count>", (t, n) =>
        {
            components.airdrop.AddCargoFromCatalogWithQuantity(t, n);
        });

        DebugCommand<int> SET_HEALTH = new DebugCommand<int>("health", "Set health", "health <n>", (n) =>
        {
            components.player.GetController<CharacterHealthSystem>().SetHealth(n);
        });

        DebugCommand<string> SHOW_HINT = new DebugCommand<string>("hint", "Show hint", "hint <s>", (s) =>
        {
            components.hint.Show(new Hint(s, Color.white));
        });

        DebugCommand RESET_HINT = new DebugCommand("Rhint", "Reset hint state", "Rhint", () =>
        {
            components.hintManager.SetUpHints();
        });

        DebugCommand FIRST_PICKED_UP = new DebugCommand("picked", "First weapon picked up", "picked", () =>
        {
            GameEvents.current.NotifyOnPickedUpForTheFirstTime();
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
            SET_DEATH_PROGRESS,
            ADD_CARGO,
            SET_HEALTH,
            SHOW_HINT,
            RESET_HINT,
            FIRST_PICKED_UP
        };
    }
}