using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{
    [SerializeField] private LimbData data;
    [SerializeField] private LimbSkin skin;
    [SerializeField] private LimbParticles particles;

    private List<IHitBox> hitBoxes;
    private GoreSFXPlayer sFXPlayer;

    public void AddHitBox(IHitBox hitBox){
        if (hitBoxes == null)
            hitBoxes = new List<IHitBox>();

        hitBoxes.Add(hitBox);
    }

    public void SetSFXPlayer(GoreSFXData goreSFXData){
        sFXPlayer = new GoreSFXPlayer(transform, goreSFXData);
    }

    private void Awake() => Initialize();

    private void Initialize()
    {
        data.Initialize();

        if (data.IsHealthy()){
            skin.ShowFlesh();
        }
        else if (data.IsInjured()){
            skin.ShowBones();
        }
    }

    public void ReceiveDamage(int damage)
    {
        data.SubtractHealth(damage);

        if (data.ReadyForMutilation()){
            Mutilate();
        }
        else if (data.ReadyForAmputation()){
            Amputate();
        }
        else{
            Damage();
        }
    }

    private void Mutilate()
    {
        data.SetStateToInjured();
        skin.ShowBones();
        particles.PlayFleshExplosion();

        sFXPlayer.PlaySmashSFX();
    }

    private void Amputate()
    {
        data.SetStateToDestroyed();
        SwitchOffHitBoxes();
        skin.Hide();
        particles.PlayBonesExplosion();

        sFXPlayer.PlaySmashSFX();
    }

    private void SwitchOffHitBoxes(){
        foreach (IHitBox hitBox in hitBoxes)
            hitBox.SwitchOff();
    }

    private void Damage()
    {
        sFXPlayer.PlayHitSFX();
    }

    public bool IsDestroyed() => data.IsDestroyed();
}


