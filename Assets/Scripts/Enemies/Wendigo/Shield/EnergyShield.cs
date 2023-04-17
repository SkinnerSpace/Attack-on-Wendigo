using System;
using UnityEngine;

public class EnergyShield : MonoBehaviour
{
    private const float FULL_ENERGY = 1f;

    [SerializeField] private MeshRenderer shieldRenderer;
    [SerializeField] private MeshRenderer bottomRenderer;
    [SerializeField] private ParticleSystem soot;

    private Material shieldMaterial;
    private Material bottomMaterial;

    private FunctionTimer timer;

    private bool isFading;
    private float energy = FULL_ENERGY;
    private float fadeSpeed = 0.05f;

    private float lerp = 0f;

    public event Action onDisappear;

    private void Awake()
    {
        timer = GetComponent<FunctionTimer>();

        shieldMaterial = shieldRenderer.material;
        bottomMaterial = bottomRenderer.material;
    }

    public void OnObjectSpawn()
    {
        isFading = false;
        energy = FULL_ENERGY;
        SetEnergy(energy);

        lerp = 0;
        soot.Play();
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
            Disappear();
        }
    }

    private void SetEnergy(float currentEnergy)
    {
        shieldMaterial.SetFloat("Energy", currentEnergy);
        bottomMaterial.SetFloat("Energy", currentEnergy);
    }

    private void Disappear()
    {
        isFading = false;

        shieldRenderer.enabled = false;
        bottomRenderer.enabled = false;

        onDisappear?.Invoke();
    }

    private void StartFading()
    {
        isFading = true;
    }
}
