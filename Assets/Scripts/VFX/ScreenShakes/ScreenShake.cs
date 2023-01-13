using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private const float DEVIATION_MULTIPLIER = 0.1f;
    private const float WAVE_UNIT = Mathf.PI * 2f;

    [SerializeField] private ShakeTimer timer;
    [SerializeField] private Shaker shaker;

    private float wave;
    private Vector3 direction;
    private Vector3 angle;
    private bool isShaking;

    private ShakeData data;

    public void Launch()
    {
        ShakeEffect effect = new ShakeEffect(1f, 8f, 0f);
        ShakeCurve curve = new ShakeCurve(1f, 0.25f, 0.25f, 10f);
        data = new ShakeData(effect, curve);

        isShaking = true;
        timer.Set(data.curve.time);
        direction = GetDirection();
        angle = GetAngle();
    }

    public void UpdateShake(float timePassed)
    {
        if (isShaking)
        {
            timer.CountDown(timePassed);
            Shake();
        }
    }

    private void Shake()
    {
        float exWave = wave;
        wave = Mathf.Sin(timer.TimeScaled * WAVE_UNIT * data.curve.frequency);

        if (exWave < 0f && wave >= 0f)
        {
            direction = ModifyDirection(direction);
        }

        float amplitude = GetAmplitude();

        Vector3 shakePosition = wave * amplitude * direction * data.effect.power;
        Vector3 shakeAngle = wave * amplitude * angle * (data.effect.power * data.effect.angleMultiplier);

        shaker.SetPosAndAngle(shakePosition, shakeAngle);
    }

    private Vector3 ModifyDirection(Vector3 dir)
    {
        Vector3 deviation = new Vector3(Rand.Range(-1f, 1f), Rand.Range(-1f, 1f), 0f).normalized * DEVIATION_MULTIPLIER;
        Vector3 modifiedDirection = (dir + deviation).normalized;

        return modifiedDirection;
    }

    private float GetAmplitude()
    {
        float amplitude = 1f;

        if (timer.TimeScaled < data.curve.attack)
        {
            amplitude = Mathf.Sqrt(timer.TimeScaled / data.curve.attack);
        }
        else if (timer.TimeScaled > data.curve.release)
        {
            amplitude = (1f - timer.TimeScaled) / (1f - data.curve.release);
            amplitude *= amplitude;
        }

        return amplitude;
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = new Vector3(Rand.Range(-1f, 1f), Rand.Range(-1f, 1f), 0f).normalized;
        return direction;
    }

    private Vector3 GetAngle()
    {
        float choice = Rand.Range(0f, 1f);
        float zAngle = choice < 0.5f ? -1 : 1f;

        return new Vector3(0f, 0f, zAngle);
    }

    public void Stop()
    {
        isShaking = false;
    }
}

public class ShakeHandler
{

}

public struct ShakeData
{
    public ShakeEffect effect { get; private set; }
    public ShakeCurve curve { get; private set; }

    public ShakeData(ShakeEffect effect, ShakeCurve curve)
    {
        this.effect = effect;
        this.curve = curve;
    }
}

public struct ShakeEffect
{
    public float power { get; private set; }
    public float angleMultiplier { get; private set; }
    public float distance { get; private set; }

    public ShakeEffect(float power, float angleMultiplier, float distance)
    {
        this.power = power;
        this.angleMultiplier = angleMultiplier;
        this.distance = distance;
    }
}

public struct ShakeCurve
{
    public float time { get; private set; }
    public float attack { get; private set; }
    public float release { get; private set; }
    public float frequency { get; private set; }

    public ShakeCurve(float time, float attack, float release, float frequency)
    {
        this.time = time;
        this.attack = attack;
        this.release = release;
        this.frequency = frequency;
    }
}

