using UnityEngine;

[CreateAssetMenu(fileName ="ParticleSoundData", menuName ="ScriptableObjects/ParticleSoundData")]
public class ParticleSoundData : ScriptableObject
{
    public FMODUnity.EventReference collisionSFX;
    public int variety;
    public float pitchRange;
}