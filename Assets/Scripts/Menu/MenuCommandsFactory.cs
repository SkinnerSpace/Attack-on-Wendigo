using System;

public static class MenuCommandsFactory
{
    public static Action Create(string interaction)
    {
        switch (interaction)
        {
            case "start":
                return MenuEvents.current.StartTheGame;

            case "continue":
                return MenuEvents.current.ContinueTheGame;

            case "restart":
                return MenuEvents.current.RestartTheGame;

            case "settings":
                return MenuEvents.current.OpenSettings;

            case "quit":
                return MenuEvents.current.QuitTheGame;

            case "back to main":
                return MenuEvents.current.BackToMenu;

            case "sound":
                return MenuEvents.current.OpenSoundSettings;

            case "back to settings":
                return MenuEvents.current.BackToSettings;

            case "controls":
                return MenuEvents.current.OpenControls;
        }

        return null;
    }
}
