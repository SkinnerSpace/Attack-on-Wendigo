using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    private static int pickUpCount;

    public static GameEvents current;

    public event Action onStart;
    public event Action onPlayerHasDied;
    public event Action onVictory;

    public event Action<float> onProgressUpdate;
    public event Action onFirstWeaponPickedUp;
    public event Action onWeaponSweptAway;

    private void Awake()
    {
        current = this;
    }

    public void ResetState()
    {
        onPlayerHasDied = null;
        onVictory = null;
        onProgressUpdate = null;
        onFirstWeaponPickedUp = null;

        pickUpCount = 0;
    }

    public void InvasionHasBegun() => onStart?.Invoke();
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

    public void WeaponHasBeenSweptAway() => onWeaponSweptAway?.Invoke();
}