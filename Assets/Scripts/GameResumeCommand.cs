﻿using UnityEngine;

public class GameResumeCommand : ICommand
{
    private GameManager gameManager;

    public GameResumeCommand(GameManager gameManager) => this.gameManager = gameManager;

    public void Execute()
    {
        GameState.PauseMode = PauseMode.None;
        AudioPlayer.ResumeAll();

        gameManager.Menu.Close();

        GameEvents.current.TheGameIsResumed();
    }
}