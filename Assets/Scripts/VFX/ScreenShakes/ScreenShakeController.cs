using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    private List<ScreenShake> shakes = new List<ScreenShake>();
    [SerializeField] private Shaker shaker;
    private ShakeHandler shakeHandler;

    private void Awake()
    {
        shakeHandler = new ShakeHandler();
    }

    private void Update()
    {
        Launch();

        foreach (ScreenShake shake in shakes)
        {
            shakeHandler.Handle(shake);
            shaker.Displace(shakeHandler.GetDisplacement(shake));
        }
    }

    private void Launch()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ShakeCurve curve = new ShakeCurve(frequency: 10f, attack: 0.25f, release: 0.25f);
            ShakeStrength strength = new ShakeStrength(1f, 8f);
            ScreenShake shake = new ScreenShake(time: 1f, strength, curve);

            shakes.Add(shake);
            shakeHandler.Launch(shake);
        }
    }
}
