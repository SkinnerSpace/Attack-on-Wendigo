using UnityEngine;

public class HouseBurnHandler : MonoBehaviour
{
    private static int soot = Shader.PropertyToID("Soot");
    private static int sootPattern = Shader.PropertyToID("SootPattern");

    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Chronos chronos;
    [SerializeField] private FireHitBox fireHitBox;
    [SerializeField] private float burnSpeed = 0.5f;

    private MaterialPropertyBlock propBlock;
    private bool isOnFire;
    private float burntness = 0f;
    
    private void Awake()
    {
        fireHitBox.Subscribe(SetOnFire, CoolDown);
        propBlock = new MaterialPropertyBlock();
        enabled = false;

        SetSootPattern();
    }

    private void SetSootPattern() => SetMaterialProperty(sootPattern, Rand.Range(0f, 1000f));

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
        {
            burntness = 1f;
            fireHitBox.SetActive(false);
            CoolDown();
        }
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

    private void SetOnFire()
    {
        isOnFire = true;
        enabled = true;
    }

    private void CoolDown()
    {
        isOnFire = false;
        enabled = false;
    }
}
