using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    private static int pickUpCount;

    public static GameEvents current;

    public event Action onInvasionHasBegun;
    public event Action onPlayerHasDied;
    public event Action onVictory;

    public event Action<float> onProgressUpdate;
    public event Action onFirstWeaponPickedUp;
    public event Action onWeaponSweptAway;

    public event Action<float> onEnemyHealthUpdate;

    public event Action onStart;
    public event Action onPause;
    public event Action onResume;

    private void Awake()
    {
        current = this;
    }

    public void ResetState()
    {
        pickUpCount = 0;

        onInvasionHasBegun = null;
        onPlayerHasDied = null;
        onVictory = null;

        onProgressUpdate = null;
        onFirstWeaponPickedUp = null;
        onWeaponSweptAway = null;

        onStart = null;
        onPause = null;
        onResume = null;
    }

    public void InvasionHasBegun() => onInvasionHasBegun?.Invoke();
    public void PlayerHasDied() => onPlayerHasDied?.Invoke();
    public void DeclareVictory() => onVictory?.Invoke();
    public void UpdateProgress(float progress) => onProgressUpdate?.Invoke(progress);
    public void WeaponHasBeenPickedUp()
    {
        if (pickUpCount == 0){
            onFirstWeaponPickedUp?.Invoke();
        }

        pickUpCount += 1;
    }

    public void EnemyHealthHasBeenUpdated(float health) => onEnemyHealthUpdate?.Invoke(health);

    public void WeaponHasBeenSweptAway() => onWeaponSweptAway?.Invoke();

    public void TheGameHasStarted() => onStart?.Invoke();
    public void TheGameIsPaused() => onPause?.Invoke();
    public void TheGameIsResumed() => onResume?.Invoke();
}