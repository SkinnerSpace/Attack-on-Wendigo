﻿using UnityEngine;

public class GUIController : MonoBehaviour
{
    [SerializeField] private GUIContainer main;
    [SerializeField] private GUIContainer hUD;
    [SerializeField] private GUIContainer aim;

    private bool playerIsActive;

    private void Start()
    {
        ConnectMain();
        ConnectHUD();
    }

    private void ConnectMain()
    {
        GameEvents.current.onGameBegun += main.ShowGradually;
        GameEvents.current.onGameBegun += () => playerIsActive = true;

        GameEvents.current.onPause += () => 
        {
            if (playerIsActive){
                main.HideImmediately();
            }
        };

        GameEvents.current.onResume += () =>
        {
            if (playerIsActive){
                main.ShowImmediately();
            }
        };


        GameEvents.current.onPlayerHasDied += hUD.HideGradually;
        GameEvents.current.onPlayerHasDied += aim.HideGradually;
    }

    private void ConnectHUD()
    {
        GameEvents.current.onVictory += hUD.HideGradually;
    }
}