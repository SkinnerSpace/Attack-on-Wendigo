using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigController : MonoBehaviour
{
    private Rig rig;

    private float targetWeight;
    private float timeOut;
    private float time;

    private bool isChanging;
    private IEnumerator change;

    private void Awake() => rig = GetComponent<Rig>();

    public void SwitchOn() => SetChangeWeightOverTime(1f, 2f);
    public void SwitchOff() => SetChangeWeightOverTime(0f, 0.5f);

    private void SetChangeWeightOverTime(float targetWeight, float timeOut)
    {
        this.targetWeight = targetWeight;
        this.timeOut = timeOut;
        
        StopPreviousChange();
        change = ChangeWeightOverTime();
        StartCoroutine(change);
    }

    private void StopPreviousChange(){
        if (isChanging) StopCoroutine(change);
    }

    private IEnumerator ChangeWeightOverTime()
    {
        time = 0f;
        isChanging = true;

        while (time < timeOut)
        {
            time += Chronos.DeltaTime;
            rig.weight = ChangeWeight(rig.weight);

            yield return null;
        }

        isChanging = false;
    }

    private float ChangeWeight(float weight)
    {
        float percent = Mathf.InverseLerp(0f, timeOut, time);
        float currentWeight = Mathf.Lerp(weight, targetWeight, percent);
        return currentWeight;
    }
}
