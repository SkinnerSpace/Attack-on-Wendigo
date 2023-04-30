using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    private int pickUpCount;

    public static GameEvents current;

    public event Action onInvasionHasBegun;
    public event Action onVictory;

    public event Action<float> onProgressUpdate;
    public event Action<float> onDeathProgressUpdate;

    public event Action onFirstWeaponPickedUp;
    public event Action onCargoIsTaken;

    public event Action<float> onEnemyHealthUpdate;
    public event Action<float, Vector3> onBlizzardRadiusUpdate;

    public event Action onStart;
    public event Action onPause;
    public event Action onResume;
    public event Action onIntroIsOver;
    public event Action onRestart;

    public event Action onBluntDamageReceived;
    public event Action onOutroCameraStoppedTracking;

    private void Awake()
    {
        current = this;
    }

    public void InvasionHasBegun() => onInvasionHasBegun?.Invoke();
    public void DeclareVictory() => onVictory?.Invoke();
    public void UpdateProgress(float progress) => onProgressUpdate?.Invoke(progress);
    public void UpdateDeathProgress(float deathProgress) => onDeathProgressUpdate?.Invoke(deathProgress);
    public void UpdateBlizzardRadius(float blizzardRadius, Vector3 position) => onBlizzardRadiusUpdate?.Invoke(blizzardRadius, position);

    public void WeaponHasBeenPickedUp()
    {
        if (pickUpCount == 0){
            onFirstWeaponPickedUp?.Invoke();
        }

        pickUpCount += 1;
    }

    public void EnemyHealthHasBeenUpdated(float health) => onEnemyHealthUpdate?.Invoke(health);

    public void CargoIsTaken() => onCargoIsTaken?.Invoke();

    public void TheGameHasStarted() => onStart?.Invoke();
    public void TheGameIsPaused() => onPause?.Invoke();
    public void TheGameIsResumed() => onResume?.Invoke();

    public void IntroIsOver() => onIntroIsOver?.Invoke();
    public void NotifyOnRestart() => onRestart?.Invoke();

    public void PlayerReceivedBluntDamage() => onBluntDamageReceived?.Invoke();
    

    public void OutroCameraStoppedTracking() => onOutroCameraStoppedTracking?.Invoke();
}
