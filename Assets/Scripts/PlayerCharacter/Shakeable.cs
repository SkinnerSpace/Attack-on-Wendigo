using UnityEngine;

public class Shakeable : MonoBehaviour, IShakeable
{
    [SerializeField] private CharacterData data;

    public void Displace(IShakeDisplacement displacement)
    {
        data.ShakePosition = displacement.Position;
        data.ShakeEuler = new Vector3(0f, 0f, displacement.Angle.z);
    }
} 

