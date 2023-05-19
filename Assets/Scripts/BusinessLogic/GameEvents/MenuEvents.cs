using System;
using UnityEngine;

public class MenuEvents : MonoBehaviour
{
    public static MenuEvents current;

    public event Action onMainMenuOpenedForTheFirstTime;
    public event Action onMenuOpened;
    public event Action onMenuClosed;

    public event Action onStart;
    public event Action onResume;
    public event Action onRestart;
    public event Action onQuit;

    public event Action<string> onSubMenuEnter;

    public bool gameHasBegun { get; private set; }

    private void Awake(){
        current = this;
    }

    public void NotifyOnMainMenuOpenedForTheFirstTime() => onMainMenuOpenedForTheFirstTime?.Invoke();
    public void NotifyOnMenuOpened() => onMenuOpened?.Invoke();
    public void NotifyOnMenuClosed() => onMenuClosed?.Invoke();

    public void StartTheGame()
    {
        onStart?.Invoke();
        gameHasBegun = true;
    }
    public void ContinueTheGame() => onResume?.Invoke();
    public void RestartTheGame() => onRestart?.Invoke();
    public void QuitTheGame() => onQuit?.Invoke();

    public void EnterSubMenu(string subMenuName) => onSubMenuEnter?.Invoke(subMenuName);
}