using UnityEngine;

public class HeadHairCutController : MonoBehaviour
{
    [SerializeField] private ParticleSystem hairParticles;
    [SerializeField] private MeshRenderer hairMesh;
    [SerializeField] private Limb headLimb;

    [Header("Hit Boxes")]
    [SerializeField] private HitBoxProxy leftHorn;
    [SerializeField] private HitBoxProxy rightHorn;
    [SerializeField] private HitBoxProxy head;

    private bool leftHornIsDamaged;
    private bool rightHornIsDamaged;

    private bool isBald;

    private void Awake()
    {
        headLimb.SubscribeOnAmputation(GoBald);

        leftHorn.SubscribeOnGettingDamage(OnLeftHornIsDamaged);
        rightHorn.SubscribeOnGettingDamage(OnRightHornIsDamaged);
        head.SubscribeOnGettingDamage(OnHeadIsDamaged);
    }

    private void OnLeftHornIsDamaged() => leftHornIsDamaged = true;
    private void OnRightHornIsDamaged() => rightHornIsDamaged = true;
    private void OnHeadIsDamaged()
    {
        if (leftHornIsDamaged && rightHornIsDamaged){
            GoBald();
        }
    }

    public void GoBald()
    {
        if (!isBald){
            isBald = true;

            hairParticles.Play();
            hairMesh.enabled = false;
        }
    }
}
