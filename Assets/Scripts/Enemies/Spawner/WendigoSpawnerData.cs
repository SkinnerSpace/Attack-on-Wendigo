﻿using UnityEngine;

public class WendigoSpawnerData
{
    public bool configurable = true;

    public int initialSpawnCount; // Configurable
    public int leftToSpawnCount;
    public int spawnedCount;
    
    public int aliveCount;
    public int deadCount;

    public int currentCount;
    public int allowedConcurrentCount; // Configurable
    public float density;

    public float timeToSpawn; // Configurable

    public float progress;

    public int health; // Configurable

    public float speed; // Configurable

    public float maxFireballDistance; // Configurable

    private WendigoSpawnConfig config;

    public WendigoSpawnerData(WendigoSpawnConfig config)
    {
        this.config = config;
        Initialize();
        UpdateGameFlowValues();
    }

    public void Initialize()
    {
        initialSpawnCount = config.initialCount;
        leftToSpawnCount = initialSpawnCount;
        spawnedCount = 0;

        currentCount = 0;
        density = 0;
        aliveCount = 0;
        deadCount = 0;
        progress = 0;
    }

    public void UpdateGameFlowValues()
    {
        if (configurable){
            allowedConcurrentCount = Mathf.RoundToInt(config.concurrentSpawnCount.Evaluate(progress));
            timeToSpawn = Mathf.Lerp(config.minTimeInterval.Evaluate(progress), config.maxTimeInterval.Evaluate(progress), density);
            health = Mathf.RoundToInt(config.health.Evaluate(progress));
            speed = config.speed.Evaluate(progress);
            maxFireballDistance = config.maxFireballDistance.Evaluate(progress);
        }
    }
}

