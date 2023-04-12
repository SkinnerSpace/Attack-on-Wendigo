using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    private static int pickUpCount;

    public static GameEvents current;

    public event Action onPlayerHasDied;
    public event Action onWendigosAreDefeated;
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
        onWendigosAreDefeated = null;
        onProgressUpdate = null;
        onFirstWeaponPickedUp = null;

        pickUpCount = 0;
    }

    public void PlayerHasDied() => onPlayerHasDied?.Invoke();
    public void WendigosAreDefeated() => onWendigosAreDefeated?.Invoke();
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