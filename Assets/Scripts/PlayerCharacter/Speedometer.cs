using UnityEngine;

public class Speedometer : MonoBehaviour
{
    public float speedMagnitude { get; private set; }

    public void MeasureSpeed(float speed, float maxSpeed) => speedMagnitude = speed / maxSpeed;
}

public interface ISpeedObserver
{
    void ConnectSpeedometer(Speedometer speedometer);
}