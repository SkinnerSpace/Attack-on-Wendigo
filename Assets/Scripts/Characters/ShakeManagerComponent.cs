using System.Collections.Generic;
using UnityEngine;

public class ShakeManagerComponent : MonoBehaviour, IShakeManager
{
    private ShakeManager manager;
    [SerializeField] private Shakeable shakeable;

    public static IShakeManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        manager = new ShakeManager(shakeable);
    }

    private void Update() => manager.Update();

    public void AddAndLaunch(Shake shake) => manager.AddAndLaunch(shake);
}
