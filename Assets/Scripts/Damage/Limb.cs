using System;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{
    [SerializeField] private LimbData data;
    [SerializeField] private LimbSkin skin;
    [SerializeField] private LimbParticles particles;

    private List<IHitBox> hitBoxes;
    private LimbSFXPlayer sFXPlayer;

    private event Action onMutilation;

    public void AddHitBox(IHitBox hitBox){
        if (hitBoxes == null)
            hitBoxes = new List<IHitBox>();

        hitBoxes.Add(hitBox);
    }

    public void SetSFXPlayer(GoreSFXData goreSFXData){
        sFXPlayer = LimbSFXPlayerFactory.Create(transform, goreSFXData, data.SFXSet);
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

    public void SubscribeOnMutilation(Action onMutilation) => this.onMutilation += onMutilation;

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
        GoBaldIfNecessary();

        particles.PlayFleshExplosion();

        sFXPlayer.PlaySmashSFX();

        onMutilation?.Invoke();
    }

    private void Amputate()
    {
        data.SetStateToDestroyed();
        SwitchOffHitBoxes();
        skin.Hide();
        GoBaldIfNecessary();

        particles.PlayBonesExplosion();

        sFXPlayer.PlaySmashSFX();
    }

    private void GoBaldIfNecessary(){
        if (data.ReadyToGoBald())
            skin.GoBald();
    }

    private void SwitchOffHitBoxes(){
        foreach (IHitBox hitBox in hitBoxes)
            hitBox.SwitchOff();
    }

    private void Damage()
    {
        //sFXPlayer.PlayHitSFX();
    }

    public bool IsDestroyed() => data.IsDestroyed();

    public void ExposeGoreButKeepTheFleshUntouched() => skin.ExposeGoreButKeepTheFleshUntouched();
}

