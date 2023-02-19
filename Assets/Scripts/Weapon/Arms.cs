using UnityEngine;

public class Arms : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    private SkinnedMeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        weapon.onReady += Show;
    }

    public void Show(bool isVisible) => meshRenderer.enabled = isVisible;
}

