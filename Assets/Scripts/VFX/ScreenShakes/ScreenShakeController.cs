using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    [SerializeField] private List<ScreenShake> shakes;

    private void Update()
    {
        Launch();

        foreach (ScreenShake shake in shakes)
        {
            shake.UpdateShake(Time.deltaTime);
        }
    }

    private void Launch()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (ScreenShake shake in shakes)
            {
                shake.Launch();
            }
        }
    }
    
    public void AddShake(ScreenShake shake)
    {
        shakes.Add(shake);
    }
}
