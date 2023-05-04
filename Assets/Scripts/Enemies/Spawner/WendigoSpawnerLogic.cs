using System;
using UnityEngine;

public class WendigoSpawnerLogic
{
    public WendigoSpawnerData data;
    private FunctionTimer timer;

    private event Action onSpawn;
    private event Action<float> onSetTimer;
    private event Action<int> onAliveCountUpdate;

    public WendigoSpawnerLogic(WendigoSpawnerData data, FunctionTimer timer)
    {
        this.data = data; ;
        this.timer = timer;
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
        UpdateProgress();

        data.leftToSpawnCount -= 1;
        data.spawnedCount += 1;
        data.currentCount += 1;
        UpdateDensity();

        onSpawn?.Invoke();

        UpdateProgress();
        NotifyOnSetTimer();

        //Debug.Log("LEFT " + data.leftToSpawnCount);
    }

    public void OnDeath()
    {
        data.currentCount -= 1;
        data.deadCount += 1;

        UpdateDensity();
        UpdateDeathProgress();
        UpdateAliveCount();
        NotifyOnSetTimer();

        DeclareVictoryIfNoOneIsLeft();

/*        Debug.Log("---COUNT---");
        Debug.Log("Alive " + data.aliveCount);
        Debug.Log("Dead " + data.deadCount);*/
    }

    public void SubscribeOnAliveCountUpdate(Action<int> onAliveCountUpdate) => this.onAliveCountUpdate += onAliveCountUpdate; 
    public void UpdateAliveCount()
    {
        data.aliveCount = data.initialSpawnCount - data.deadCount;
        onAliveCountUpdate?.Invoke(data.aliveCount);
    }

    private void NotifyOnSetTimer()
    {
        float timeToSpawn = data.currentCount > 0 ? data.timeToSpawn : 1f;
        onSetTimer?.Invoke(timeToSpawn);
    }

    private void UpdateDensity()
    {
        data.density = data.currentCount / (float)data.allowedConcurrentCount;
        data.density = Easing.QuadEaseIn(data.density);
    }

    private void UpdateProgress()
    {
        data.progress = data.spawnedCount / (float)data.initialSpawnCount;
        data.UpdateGameFlowValues();

        GameEvents.current.UpdateProgress(data.progress);
    }

    private void UpdateDeathProgress()
    {
        data.deathProgress = data.deadCount / (float)data.initialSpawnCount;
        GameEvents.current.UpdateDeathProgress(data.deathProgress);
    }

    private void DeclareVictoryIfNoOneIsLeft()
    {
        if (data.aliveCount <= 0)
        {
            GameEvents.current.DeclareVictory();
        }
            /*        if (data.aliveCount <= 0){
                        timer.Set("Victory", 2f, GameEvents.current.DeclareVictory);
                    }*/
    }
}
