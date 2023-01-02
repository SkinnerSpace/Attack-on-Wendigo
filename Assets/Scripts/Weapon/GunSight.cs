using System;
using UnityEngine;

public class GunSight : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private LayerMask ignoreLayers = ~(1 << 13);

    public IDamageable Damageable { get; private set; }
    public RaycastHit Hit => hit;
    private RaycastHit hit;

    public Action<bool> onTarget;

    private void Start()
    {
        onTarget += Aim.Instance.TargetUpdate;
    }

    private void Update()
    {
        TakeAim();
    }

    private void TakeAim()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ignoreLayers))
        {
            Damageable = hit.transform.GetComponent<IDamageable>();
        }
        else
        {
            Damageable = null;
        }

        onTarget?.Invoke(Damageable != null);
    }
}
