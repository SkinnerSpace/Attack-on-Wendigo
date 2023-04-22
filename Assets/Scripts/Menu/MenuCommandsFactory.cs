using System;

public static class MenuCommandsFactory
{
    public static Action Create(MenuCommands interaction)
    {
        switch (interaction)
        {
            case MenuCommands.Start:
                return MenuEvents.current.StartTheGame;

            case MenuCommands.Continue:
                return MenuEvents.current.ContinueTheGame;

            case MenuCommands.Restart:
                return MenuEvents.current.RestartTheGame;

            case MenuCommands.Settings:
                return MenuEvents.current.OpenSettings;

            case MenuCommands.Quit:
                return MenuEvents.current.QuitTheGame;

            case MenuCommands.BackToMenu:
                return MenuEvents.current.BackToMenu;
        }

        return null;
    }
}
