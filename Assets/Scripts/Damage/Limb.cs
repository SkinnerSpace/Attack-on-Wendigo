using System;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{
    public DamageModifiers damageModifiers;
    [SerializeField] private LimbData data;
    [SerializeField] private LimbSkin skin;
    [SerializeField] private LimbParticles particles;

    private List<IHitBox> hitBoxes;
    private LimbSFXPlayer sFXPlayer;

    public event Action onInjury;
    private event Action onMutilation;
    private event Action onAmputation;

    public event Action<DamagePackage> onDamage;

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
    public void SubscribeOnAmputation(Action onAmputation) => this.onAmputation += onAmputation;

    public void ReceiveDamage(DamagePackage damagePackage)
    {
        data.RememberCurrentState();
        data.SubtractHealth(damagePackage.damage);

        if (data.ReadyForMutilation()){
            Mutilate();
        }
        else if (data.ReadyForAmputation()){
            Amputate();
        }
        else{
            Damage();
        }

        NotifyOnDamage(damagePackage);
    }

    private void Mutilate()
    {
        data.SetStateToInjured();
        skin.ShowBones();
        GoBaldIfNecessary();

        particles.PlayFleshExplosion();

        sFXPlayer.PlaySmashSFX();

        onInjury?.Invoke();
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

        onInjury?.Invoke();
        onAmputation?.Invoke();
    }

    private void Damage()
    {
        //sFXPlayer.PlayHitSFX();
        GoBaldIfNecessary();
    }

    private void GoBaldIfNecessary(){
        if (data.ReadyToGoBald())
        {
            data.isBald = true;
            particles.PlayHairCut();
            skin.GoBald();
        }
    }

    public void SwitchOffHitBoxes(){
        foreach (IHitBox hitBox in hitBoxes)
            hitBox.SwitchOff();
    }

    public void MakeDestroyable(){
        data.canBeDestroyed = true;
    }

    public void MakeFragile(){
        data.isFragile = true;
    }

    public bool IsDestroyed() => data.IsDestroyed();

    public void ExposeGoreButKeepTheFleshUntouched() => skin.ExposeGoreButKeepTheFleshUntouched();

    public void ResetState()
    {
        data.ResetState();

        if (data.IsHealthy()){
            skin.ShowFlesh();
        }
        else if (data.IsInjured()){
            skin.ShowBones();
        }

        skin.GrowHair();
    }

    public void SetHealthMultiplier(int healthMultipler) => data.SetHealthMultiplier(healthMultipler);

    private void NotifyOnDamage(DamagePackage damagePackage){
        DamagePackage modifiedDamagePackage = damagePackage;

        if (data.StateHasBeenChanged()){
            ModifyDamage(modifiedDamagePackage, damageModifiers.mutilation);
        }
        else{
            if (data.IsHealthy()){
                ModifyDamage(modifiedDamagePackage, damageModifiers.flesh);
            }
            else if (data.IsInjured()){
                ModifyDamage(modifiedDamagePackage, damageModifiers.bones);
            }
        }

        onDamage?.Invoke(modifiedDamagePackage);
    }

    private void ModifyDamage(DamagePackage damagePackage, float modifier){
        damagePackage.damage = Mathf.RoundToInt(damagePackage.damage * modifier);
    }
}
