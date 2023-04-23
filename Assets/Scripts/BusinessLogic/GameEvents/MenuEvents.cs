using System;
using UnityEngine;

public class MenuEvents : MonoBehaviour
{
    public static MenuEvents current;

    public event Action onStart;
    public event Action onResume;
    public event Action onRestart;
    public event Action onQuit;

    public event Action<string> onSubMenuEnter;

    private void Awake(){
        current = this;
    }

    public void StartTheGame() => onStart?.Invoke();
    public void ContinueTheGame() => onResume?.Invoke();
    public void RestartTheGame() => onRestart?.Invoke();
    public void QuitTheGame() => onQuit?.Invoke();

    public void EnterSubMenu(string subMenuName) => onSubMenuEnter?.Invoke(subMenuName);
}