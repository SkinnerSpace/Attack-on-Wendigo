using UnityEngine;

public class WeaponLatch : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private WeaponData data;
    [SerializeField] private AnimationCurve animationCurve;

    private bool isPlaying;
    private float time;

    private void Start()
    {
        weapon.SubscribeOnShot(Play);
    }

    private void Update()
    {
        if (isPlaying)
        {
            CountDown();

            float lerp = Mathf.InverseLerp(0f, data.Rate, time);
            float zPosition = animationCurve.Evaluate(lerp);

            transform.localPosition = new Vector3(0f, 0f, zPosition);
        }
    }

    private void CountDown()
    {
        time += Time.deltaTime;

        if (time >= data.Rate)
        {
            isPlaying = false;
        }
    }

    private void Play()
    {
        time = 0f;
        isPlaying = true;
    }
}