using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private float maxIntensity = 1f;
    [SerializeField] private float maxTime = 1f;

    private bool isShaking;
    private float intensity;
    private float time = 0f;
    private float lerp = 0f;

    private void Update()
    {
        Shake();
        UpdateShake();
    }

    private void Shake()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            isShaking = true;
            intensity = maxIntensity;
            time = 0f;
            lerp = 0f;
        }
    }

    private void UpdateShake()
    {
        if (isShaking)
        {
            intensity = Mathf.Lerp(maxIntensity, 0f, lerp);

            Vector3 shakeDirection = GetShakeDirection();
            transform.localPosition = shakeDirection * intensity;

            CountDown();
        }
    }

    private void CountDown()
    {
        time += Time.deltaTime;
        lerp = time / maxTime;

        if (time >= maxTime) Stop();
    }

    private void Stop()
    {
        isShaking = false;
        time = 0f;
        lerp = 0f;

        transform.localPosition = Vector3.zero;
    }

    private Vector3 GetShakeDirection()
    {
        return new Vector3(Rand.Range(-1f, 1f), 0f, Rand.Range(-1f, 1f));
    }
}

public static class Rand
{
    public static float Range(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }
}
