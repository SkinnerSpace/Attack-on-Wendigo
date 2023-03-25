using UnityEngine;

public class Limb : MonoBehaviour
{
    private float INJURY_THRESHOLD = 0.5f;
    private float AMPUTATION_THRESHOLD = 0.1f;

    private enum States
    {
        Flesh,
        Bones,
        Destroyed
    }

    [Header("Settings")]
    [SerializeField] private int initialHealth;
    [SerializeField] private Limb lockLimb;

    [Header("Meshes")]
    [SerializeField] private SkinnedMeshRenderer fleshMesh;
    [SerializeField] private SkinnedMeshRenderer musclesMesh;
    [SerializeField] private SkinnedMeshRenderer bonesMesh;

    [Header("Particles")]
    [SerializeField] private ParticleSystem fleshExplosion;
    [SerializeField] private ParticleSystem bonesExplosion;

    private IHitBox hitBox;
    private GoreSFXPlayer sFXPlayer;

    private int health;

    private States state = States.Flesh;
    private float healthPercent;

    public void SetHitBox(IHitBox hitBox) => this.hitBox = hitBox;

    private void Awake()
    {
        health = initialHealth;
    }

    public void ReceiveDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
        }

        healthPercent = Mathf.InverseLerp(0f, initialHealth, health);

        if (ReadyForMutilation()){
            Mutilate();
        }
        else if (ReadyForAmputation()){
            Amputate();
        }
        else{
            Damage();
        }
    }

    private bool ReadyForMutilation(){
        return healthPercent <= INJURY_THRESHOLD && 
               state == States.Flesh
               ;
    }

    private bool ReadyForAmputation(){
        return healthPercent <= AMPUTATION_THRESHOLD && 
               state == States.Bones &&
               (lockLimb == null || 
               lockLimb.IsDestroyed())
               ;
    }

    private void Mutilate()
    {
        state = States.Bones;
        fleshMesh.enabled = false;
        musclesMesh.enabled = true;
        bonesMesh.enabled = true;

        fleshExplosion.Play();
        sFXPlayer.PlaySmashSFX();
    }

    private void Amputate()
    {
        state = States.Destroyed;
        hitBox.SwitchOff();

        musclesMesh.enabled = false;
        bonesMesh.enabled = false;

        bonesExplosion.Play();
        sFXPlayer.PlaySmashSFX();
    }

    private void Damage()
    {
        sFXPlayer.PlayHitSFX();
    }

    public void SetSFXPlayer(GoreSFXData goreSFXData)
    {
        sFXPlayer = new GoreSFXPlayer(transform, goreSFXData);
    }

    public bool IsDestroyed() => state == States.Destroyed;
}
