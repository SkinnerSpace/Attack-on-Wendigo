using System;

public class WendigoSpawnerLogic
{
    public WendigoSpawnerData data;

    private event Action onSpawn;
    private event Action<float> onSetTimer;
    private event Action<int> onAliveCountUpdate;

    public WendigoSpawnerLogic(WendigoSpawnerData data)
    {
        this.data = data; ;
    }

    public void SpawnIfPossible()
    {
        if (IsAllowedToSpawn()){
            Spawn();
        }
    } 
    
    private bool IsAllowedToSpawn()
    {
        return data.leftToSpawnCount > 0 && 
               data.currentCount < data.allowedConcurrentCount;
    }

    public void SubscribeOnSpawn(Action onSpawn) => this.onSpawn += onSpawn;
    public void SubscribeOnSetSpawnTimer(Action<float> onSetTimer) => this.onSetTimer += onSetTimer;

    public void Spawn()
    {
        data.leftToSpawnCount -= 1;
        data.spawnedCount += 1;
        data.currentCount += 1;
        UpdateDensity();

        onSpawn?.Invoke();

        UpdateProgress();
        NotifyOnSetTimer();
    }


    public void OnDeath()
    {
        data.currentCount -= 1;
        data.deadCount += 1;

        UpdateDensity();
        UpdateAliveCount();
        NotifyOnSetTimer();
    }

    public void SubscribeOnAliveCountUpdate(Action<int> onAliveCountUpdate) => this.onAliveCountUpdate += onAliveCountUpdate; 
    public void UpdateAliveCount()
    {
        data.aliveCount = data.initialSpawnCount - data.deadCount;
        onAliveCountUpdate?.Invoke(data.aliveCount);
    }

    private void NotifyOnSetTimer() => onSetTimer?.Invoke(data.timeToSpawn);

    private void UpdateDensity()
    {
        data.density = data.currentCount / (float)data.allowedConcurrentCount;
        data.density = Easing.QuadEaseIn(data.density);
    }

    private void UpdateProgress()
    {
        data.progress = data.spawnedCount / (float) data.initialSpawnCount;
        data.UpdateGameFlowValues();
    }
}
