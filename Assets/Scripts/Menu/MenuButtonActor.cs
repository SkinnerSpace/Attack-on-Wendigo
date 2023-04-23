using UnityEngine;
using System;

public class MenuButtonActor : MenuButton
{
    public enum Actions
    {
        Start,
        Resume,
        Restart,
        Quit
    }

    [SerializeField] private Actions action;

    private void Start()
    {
        onCommand = GetCommand(action);
    }

    private Action GetCommand(Actions action)
    {
        switch (action)
        {
            case Actions.Start:
                return MenuEvents.current.StartTheGame;

            case Actions.Resume:
                return MenuEvents.current.ContinueTheGame;

            case Actions.Restart:
                return MenuEvents.current.RestartTheGame;

            case Actions.Quit:
                return MenuEvents.current.QuitTheGame;
        }

        return null;
    }
}
