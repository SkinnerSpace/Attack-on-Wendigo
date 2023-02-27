using UnityEngine;

public class Arms : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    private SkinnedMeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        weapon.SubscribeOnReady(OnReady);
    }

    public void OnReady(bool isVisible) => meshRenderer.enabled = isVisible;
}

