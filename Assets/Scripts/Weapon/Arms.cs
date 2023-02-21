using UnityEngine;

public class Arms : MonoBehaviour, IWeaponObserver
{
    [SerializeField] private Weapon weapon;
    private SkinnedMeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        weapon.Subscribe(this);
    }

    public void OnReady(bool isVisible) => meshRenderer.enabled = isVisible;
}

