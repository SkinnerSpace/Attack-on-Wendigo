using System;
using UnityEngine;

public class GunSight : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private LayerMask ignoreLayers = ~(1 << 13 | 1 << 14);

    public IDamageable Damageable { get; private set; }
    public RaycastHit Hit => hit;
    private RaycastHit hit;

    public Action<bool> onTarget;

    public bool targetExist { get; private set; }

    private void Start() => SubscribeObservers();

    private void Update() => TakeAim();

    private void SubscribeObservers()
    {
        onTarget += Aim.Instance.TargetUpdate;
    }

    private void TakeAim()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out hit, Mathf.Infinity, ignoreLayers);
        Damageable = hit.transform != null ? hit.transform.GetComponent<IDamageable>() : null;

        targetExist = (Damageable != null);
        onTarget?.Invoke(targetExist);
    }
}


