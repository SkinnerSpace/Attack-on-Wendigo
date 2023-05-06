using UnityEngine;

public class MagnumDrum : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private Weapon weapon;
    [SerializeField] private WeaponPooledObject pooledObject;
    [SerializeField] private FunctionTimer timer;

    [Header("Settings")]
    [SerializeField] private float step = 60f;
    [SerializeField] private float rotationTime = 0.3f;
    [SerializeField] private float delay = 0.2f;

    [Header("SFX references")]
    [SerializeField] private FMODUnity.EventReference rotationSFX;

    private Vector3 defaultAngle;
    private Vector3 targetAngle;
    private Vector3 currentAngle;

    private float time;
    private int turns;
    private bool isRotating;

    private AudioPlayer rotationAudioPlayer;

    private void Awake()
    {
        rotationAudioPlayer = AudioPlayer.Create(rotationSFX).WithPitch(-2f, 2f);
    }

    private void Start()
    {
        defaultAngle = transform.localEulerAngles;
        weapon.SubscribeOnShot(WaitAndRotate);
        pooledObject.SubscribeOnSpawn(ResetState);
    }

    private void Update()
    {
        if (isRotating)
        {
            CountDown();
            UpdateAngle();
        }
    }

    private void CountDown()
    {
        time += Time.deltaTime;

        if (time >= rotationTime)
        {
            isRotating = false;
        }
    }

    private void UpdateAngle()
    {
        float lerp = Mathf.InverseLerp(0f, rotationTime, time);
        lerp = Easing.QuadEaseInOut(lerp);
        transform.localEulerAngles = Vector3.Lerp(currentAngle, targetAngle, lerp);
    }

    private void WaitAndRotate()
    {
        timer.Set("Rotate", delay, Rotate);
    }

    private void Rotate()
    {
        turns += 1;
        float zAngle = step * turns;
        targetAngle = defaultAngle + new Vector3(0f, 0f, zAngle);
        currentAngle = transform.localEulerAngles;

        time = 0f;
        isRotating = true;

        rotationAudioPlayer.PlayOneShot();
    }

    private void ResetState()
    {
        turns = 0;
        time = 0f;
        isRotating = false;

        targetAngle = defaultAngle;
        currentAngle = defaultAngle;
        transform.localEulerAngles = defaultAngle;
    }
}
