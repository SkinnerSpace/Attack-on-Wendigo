using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScreenShakeController : MonoBehaviour
{
    private List<ScreenShake> shakes = new List<ScreenShake>();
    [SerializeField] private Shakeable shaker;

    public static ScreenShakeController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update() => UpdateShakes();

    private void UpdateShakes()
    {
        for (int i = shakes.Count - 1; i >= 0; i--)
        {
            HandleAllActive(i);
        }
    }

    private List<ShakeDisplacement> displacements = new List<ShakeDisplacement>();

    private void HandleAllActive(int i)
    {
        displacements.Clear();

        if (shakes[i].isActive)
        {
            ShakeHandler.Handle(shakes[i]);
            shaker.Displace(ShakeHandler.GetDisplacement(shakes[i]));

            Accumulate(ShakeHandler.GetDisplacement(shakes[i]));
        }
        else if (!shakes[i].isActive)
        {
            shakes.RemoveAt(i);
        }

        if (displacements.Count > 4) ShowAccumulated();
    }

    private void Accumulate(ShakeDisplacement displacement)
    {
        displacements.Add(displacement);
    }

    private void ShowAccumulated()
    {
        Debug.Log("DISPLACEMENT -----------");
        foreach (ShakeDisplacement displacement in displacements)
        {
            Debug.Log(displacement.position);
        }
        Debug.Log("------------");
    }

    public void AddAndLaunch(ScreenShake shake)
    {
        shakes.Add(shake);
        ShakeHandler.Launch(shake);
        Debug.Log("Count " + shakes.Count);
    }
}
