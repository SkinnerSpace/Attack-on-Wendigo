using System.Collections.Generic;
using UnityEngine;

public class ShakeManager : MonoBehaviour, IShakeManager
{
    private List<Shake> shakes = new List<Shake>();
    private ShakeDisplacement displacementSum = new ShakeDisplacement();

    [SerializeField] private Shakeable shakeable;

    public static IShakeManager Instance { get; private set; }

    private void Awake() => Instance = this;

    private void Update() => UpdateShakes();

    private void UpdateShakes()
    {
        displacementSum.Clear();

        for (int i = shakes.Count - 1; i >= 0; i--)
            HandleAllActive(i);

        shakeable.Displace(displacementSum);
    }

    private void HandleAllActive(int i)
    {
        if (shakes[i].isActive)
        {
            ShakeHandler.Handle(shakes[i]);
            displacementSum.Accumulate(ShakeHandler.GetDisplacement(shakes[i]));
        }
        else if (!shakes[i].isActive)
        {
            shakes.RemoveAt(i);
        }
    }

    public void AddAndLaunch(Shake shake)
    {
        shakes.Add(shake);
        ShakeHandler.Launch(shake);
    }
}
