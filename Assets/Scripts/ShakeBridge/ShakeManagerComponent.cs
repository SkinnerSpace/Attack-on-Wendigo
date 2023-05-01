using System.Collections.Generic;
using UnityEngine;

public class ShakeManagerComponent : MonoBehaviour, IShakeManager
{
    [SerializeField] private Transform shakeableImp;

    private ShakeManager manager;
    private IShakeable shakeable;

    public static IShakeManager Instance { get; private set; }

    private void Awake()
    {
        shakeable = shakeableImp.GetComponent<IShakeable>();

        Instance = this;
        manager = new ShakeManager(shakeable);
    }

    private void Update() => manager.Update();

    public void Handle(IShake shake) => manager.Handle(shake);

    public void AddAndLaunch(IShake shake) => manager.AddAndLaunch(shake);
    public void Stop() => manager.Stop();
}
