using UnityEngine;

public class HelicopterDistanceIllusion : MonoBehaviour, ISwitchable
{
    private const float FLEW_AWAY_THRESHOLD = 0.4f;

    [SerializeField] private float illusionTime;
    private bool isActive;
    private bool flewAway;

    private float time;
    private float illusionInfluence;

    private void Start(){
        GameEvents.current.onOutroCameraStoppedTracking += SwitchOn;
    }

    private void Update()
    {
        if (isActive)
        {
            CountDown();
            NotifyOnFlewAway();

            illusionInfluence = Mathf.InverseLerp(0f, illusionTime, time);
            illusionInfluence = Easing.QuadEaseIn(illusionInfluence);
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, illusionInfluence);
        }
    }

    private void CountDown(){
        time += Time.deltaTime;
        if (time >= illusionTime)
        {
            SwitchOff();
        }
    }

    private void NotifyOnFlewAway()
    {
        if (!flewAway && illusionInfluence >= FLEW_AWAY_THRESHOLD){
            flewAway = true;
            HelicopterEvents.current.NotifyOnFlewAway();
        }
    }

    public void SwitchOn(){
        isActive = true;
        time = 0f;
        illusionInfluence = 0f;
    }

    public void SwitchOff() => isActive = false;
}