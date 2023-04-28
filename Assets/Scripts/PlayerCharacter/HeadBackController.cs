using Character;
using UnityEngine;

public class HeadBackController : MonoBehaviour
{
    private float MAX_ANGLE = 75f;

    [SerializeField] private CharacterData data;
    [SerializeField] private FunctionTimer timer;
    [SerializeField] private float maxTime = 1.5f;
    [SerializeField] private float delay = 0.5f;

    private Vector3 originalAngle;
    private Vector3 targetAngle;

    private bool isTiltingBack;
    private float targetTime;
    private float time;
    private float completeness;

    private float speed;

    private void Start(){
        PlayerEvents.current.onDeath += HeadBack;
    }

    private void Update()
    {
        if (isTiltingBack){
            time += speed * Time.deltaTime;

            speed += 0.35f * Time.deltaTime;
            if (speed >= 1f){
                speed = 1f;
            }

            completeness = Mathf.InverseLerp(0f, targetTime, time);
            completeness = Easing.QuadEaseOut(completeness);

            if (completeness >= 1f){
                isTiltingBack = false;
            }

            Vector3 currentAngle = Vector3.Lerp(originalAngle, targetAngle, completeness);
            data.CameraViewEuler = currentAngle;
        }
    }

    private void HeadBackWithDelay(){
        timer.Set("HeadBack", delay, HeadBack);
    }

    private void HeadBack()
    {
        isTiltingBack = true;

        originalAngle = data.CameraViewEuler;
        targetAngle = new Vector3(-MAX_ANGLE, originalAngle.y, originalAngle.z);
        float currentCompleteness = 1f - Mathf.InverseLerp(MAX_ANGLE, -MAX_ANGLE, data.CameraViewEuler.x);
        targetTime = maxTime * currentCompleteness;
    }
}