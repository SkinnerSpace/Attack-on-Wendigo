using UnityEngine;

public class HelicopterElevator : MonoBehaviour
{
    [SerializeField] private Helicopter helicopter;
    [SerializeField] private HelicopterData data;

    private void Start(){
        HelicopterEvents.current.onIsGoingToSetOff += Ascend;
    }

    public void Descend()
    {
        data.state = HelicopterStates.Descend;
        data.positionBeforeDescend = data.position;
        data.descendPosition = new Vector3(data.position.x, data.descendHeight, data.position.z);
    }

    public void Ascend(){
        data.state = HelicopterStates.Ascend;
    }

    private void Update()
    {
        switch (data.state)
        {
            case HelicopterStates.Descend:
                UpdateDescending();
                break;

            case HelicopterStates.Ascend:
                UpdateAscending();
                break;
        }
    }

    private void UpdateDescending()
    {
        data.currentDescendTime += Time.deltaTime;
        if (data.currentDescendTime >= data.descendTime){
            data.state = HelicopterStates.Landed;
            helicopter.NotifyOnLanded();
        }

        float lerp = Mathf.InverseLerp(0f, data.descendTime, data.currentDescendTime);
        lerp = Easing.QuadEaseInOut(lerp);

        data.position = Vector3.Lerp(data.positionBeforeDescend, data.descendPosition, lerp);
    }

    private void UpdateAscending()
    {
        data.currentAscendTime += Time.deltaTime;
        if (data.currentAscendTime >= data.ascendTime){
            data.state = HelicopterStates.Escape;
            helicopter.Escape();
        }

        float lerp = Mathf.InverseLerp(0f, data.ascendTime, data.currentAscendTime);
        lerp = Easing.QuadEaseInOut(lerp);

        data.position = Vector3.Lerp(data.descendPosition, data.positionBeforeDescend, lerp);
    }
}
