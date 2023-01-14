using UnityEngine;

public class Shaker : MonoBehaviour
{
    public void Displace(ShakeDisplacement shakePack)
    {
        transform.localPosition = shakePack.position;
        transform.localEulerAngles = shakePack.angle;
    }
} 

