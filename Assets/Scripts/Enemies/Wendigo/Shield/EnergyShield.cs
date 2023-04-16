using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShield : MonoBehaviour
{
    [SerializeField] private MeshRenderer shieldRenderer;
    [SerializeField] private MeshRenderer bottomRenderer;
    [SerializeField] private ParticleSystem soot;
    private Material shieldMaterial;
    private Material bottomMaterial;

    private FunctionTimer timer;

    private bool isFading;
    private float energy = 1f;
    private float fadeSpeed = 0.05f;

    private float lerp = 0f;

    private void Awake()
    {
        timer = GetComponent<FunctionTimer>();

        shieldMaterial = shieldRenderer.material;
        bottomMaterial = bottomRenderer.material;
        soot.Play();

        SetEnergy(1f);
        timer.Set("Fade", 0.5f, StartFading);

    }

    private void Update() => Fade();
    private void Fade()
    {
        if (isFading)
        {
            lerp += fadeSpeed * Time.deltaTime;
            SetEnergyOrDisappear();
        }
    }

    private void SetEnergyOrDisappear()
    {
        if (lerp < 1f)
        {
            SetEnergy(energy);
            energy = Mathf.Lerp(energy, 0f, lerp);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SetEnergy(float currentEnergy)
    {
        shieldMaterial.SetFloat("Energy", currentEnergy);
        bottomMaterial.SetFloat("Energy", currentEnergy);
    }

    private void StartFading()
    {
        isFading = true;
    }
}
