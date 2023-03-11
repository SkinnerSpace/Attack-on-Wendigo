using UnityEngine;

public class ObjectBurnHandler : MonoBehaviour, IInflammable
{
    private static int soot = Shader.PropertyToID("Soot");
    private static int sootPattern = Shader.PropertyToID("SootPattern");

    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private BoxCollider hitBox;
    [SerializeField] private Chronos chronos;
    [SerializeField] private ParticleSystem flameVFX;
    [SerializeField] private BurnSFXPlayer burnSFXPlayer;
    [SerializeField] private float burnSpeed = 0.5f;

    private MaterialPropertyBlock propBlock;
    private bool isOnFire;
    private float burntness = 0f;
    
    private void Awake()
    {
        propBlock = new MaterialPropertyBlock();
        enabled = false;

        SetSootPattern();
    }

    private void SetSootPattern() => SetMaterialProperty(sootPattern, Rand.Range(0f, 1000f));

    public void SetOnFire()
    {
        hitBox.enabled = false;

        flameVFX.Play();
        burnSFXPlayer.Play();

        isOnFire = true;
        enabled = true;
    }

    private void Update() => Burn();

    private void Burn()
    {
        if (isOnFire)
        {
            HeatUpUntilBurnt();
            ApplyBurntness();
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
        burnSFXPlayer.Stop();

        burntness = 1f;
        isOnFire = false;
        enabled = false;
    }

    private void ApplyBurntness()
    {
        float smoothBrntness = Easing.QuadEaseInOut(burntness);
        SetMaterialProperty(soot, smoothBrntness);
    }

    private void SetMaterialProperty(int prop, float value)
    {
        mesh.GetPropertyBlock(propBlock);
        propBlock.SetFloat(prop, value);
        mesh.SetPropertyBlock(propBlock);
    }
}
