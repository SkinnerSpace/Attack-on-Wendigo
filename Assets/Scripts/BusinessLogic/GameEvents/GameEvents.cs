using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    public event Action onPlayerHasDied;
    public event Action onWendigosAreDefeated;

    private void Awake()
    {
        current = this;
    }

    public void PlayerHasDied() => onPlayerHasDied?.Invoke();
    public void WendigosAreDefeated() => onWendigosAreDefeated?.Invoke();
}