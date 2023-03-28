using UnityEngine;

[CreateAssetMenu(fileName ="GoreSFXData", menuName ="ScriptableObjects/GoreSFXData", order =1)]
public class GoreSFXData : ScriptableObject
{
    [Header("Flesh")]
    public FMODUnity.EventReference fleshHitSFX;
    public FMODUnity.EventReference fleshSmashSFX;

    [Header("Bones")]
    public FMODUnity.EventReference bonesHitSFX;
    public FMODUnity.EventReference bonesSmashSFX;
}
