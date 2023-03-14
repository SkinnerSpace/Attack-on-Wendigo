using UnityEngine;

public class Shakeable : MonoBehaviour
{
    [SerializeField] private CharacterData data;

    public void Displace(ShakeDisplacement displacement)
    {
        data.ShakePosition = displacement.position;
        data.ShakeEuler = new Vector3(0f, 0f, displacement.angle.z); //GetModifiedAngle(displacement.angle.z);

        //Debug.Log("Pos " + data.ShakePosition + " Angle " + data.ShakeEuler);

    /*    if (positionAcceptor != null) 
            positionAcceptor.localPosition = displacement.position;

        if (angleAcceptor != null) 
            angleAcceptor.localEulerAngles = GetModifiedAngle(displacement.angle.z);*/
    }

/*    private Vector3 GetModifiedAngle(float zAngle)
    {
        return new Vector3(angleAcceptor.localEulerAngles.x, 
                           angleAcceptor.localEulerAngles.y, 
                           zAngle);
    }*/
} 

