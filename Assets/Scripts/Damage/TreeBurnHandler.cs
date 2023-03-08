using System.Collections.Generic;
using UnityEngine;

public class TreeBurnHandler : MonoBehaviour
{
    private static int baseColor = Shader.PropertyToID("_BaseColor");
    private static int cutOff = Shader.PropertyToID("_Cutoff");

    [SerializeField] private Chronos chronos;
    [SerializeField] private FireHitBox fireHitBox;
    [SerializeField] private ParticleSystem flame;
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
        fireHitBox.Subscribe(SetOnFire);
        propBlock = new MaterialPropertyBlock();
        enabled = false;
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

    private void SetOnFire()
    {
        fireHitBox.SetActive(false);
        flame.Play();
        isOnFire = true;
        enabled = true;
    }
}
