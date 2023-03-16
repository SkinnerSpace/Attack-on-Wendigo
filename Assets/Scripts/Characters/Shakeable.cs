using UnityEngine;

public class Shakeable : MonoBehaviour, IShakeable
{
    [SerializeField] private CharacterData data;

    public void Displace(ShakeDisplacement displacement)
    {
        data.ShakePosition = displacement.position;
        data.ShakeEuler = new Vector3(0f, 0f, displacement.angle.z);
    }
} 

