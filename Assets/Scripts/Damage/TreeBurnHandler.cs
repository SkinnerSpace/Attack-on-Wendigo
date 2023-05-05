using System.Collections.Generic;
using UnityEngine;

public class TreeBurnHandler : MonoBehaviour, IInflammable
{
    private static int baseColor = Shader.PropertyToID("_BaseColor");
    private static int cutOff = Shader.PropertyToID("_Cutoff");

    [SerializeField] private Chronos chronos;
    [SerializeField] private BoxCollider hitBox;
    [SerializeField] private ParticleSystem flameVFX;
    [SerializeField] private float burnSpeed = 0.5f;

    [SerializeField] private MeshRenderer mesh;
    private MaterialPropertyBlock propBlock;

    private bool isOnFire;
    private float burntness;
    private float smoothBurntness;

    private Color burnColor;
    private float burnCutOff;

    private void Awake()
    {
        propBlock = new MaterialPropertyBlock();
        enabled = false;
    }

    public void InflameDirectly(Vector3 flamePoint) => SetOnFire();

    public void SetOnFire()
    {
        hitBox.enabled = false;

        flameVFX.Play();
        isOnFire = true;
        enabled = true;
    }

    private void Update() => Burn();

    private void Burn()
    {
        if (isOnFire)
        {
            HeatUpUntilBurnt();
            CalculateFireDamage();
            ApplyBurntness(burnColor, burnCutOff);
        }
    }

    private void HeatUpUntilBurnt()
    {
        burntness += burnSpeed * chronos.DeltaTime;

        if (burntness >= 1f)
            BurnDown();
    }

    private void BurnDown()
    {
        burntness = 1f;
        isOnFire = false;
        enabled = false;
    }

    private void CalculateFireDamage()
    {
        smoothBurntness = Easing.QuadEaseIn(burntness);
        burnColor = Color.Lerp(Color.white, Color.black, smoothBurntness);
        burnCutOff = Mathf.Lerp(0.01f, 1f, smoothBurntness);
    }

    private void ApplyBurntness(Color burnColor, float burnCutoff)
    {
        mesh.GetPropertyBlock(propBlock);
        propBlock.SetColor(baseColor, burnColor);
        propBlock.SetFloat(cutOff, burnCutoff);
        mesh.SetPropertyBlock(propBlock);
    }
}
