using UnityEngine;

public class Shaker : MonoBehaviour
{
    public void SetPosAndAngle(Vector3 shakePosition, Vector3 shakeAngle)
    {
        transform.localPosition = shakePosition;
        transform.localEulerAngles = shakeAngle;
    }
} 

