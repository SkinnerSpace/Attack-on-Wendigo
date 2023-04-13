using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimPresenter : MonoBehaviour
{
    [SerializeField] private Aim aim;
    [SerializeField] private UGUIElement uGUIElement;
    [SerializeField] private AimAnimator animator;

    [Header("Colors")]
    [SerializeField] private Color onTargetColor;
    [SerializeField] private Color offTargetColor;
    [SerializeField] private Color hitColor;

    private List<Transform> targets = new List<Transform>();

    public static AimPresenter Instance;

    private bool interactiveItemExist;
    private bool targetExist;

    public void OnShot() => animator.PlayShoot();

    private void Awake()
    {
        Instance = this;

        if (uGUIElement != null){
            uGUIElement.Subscribe(OnInteractiveItemUpdate);
        }

        SetOffTarget();
    }

    public void AddTarget(Transform target)
    {
        targets.Add(target);
        UpdateAimTargetState();
    }
    public void RemoveTarget(Transform target)
    {
        targets.Remove(target);
        UpdateAimTargetState();
    }

    public void OnInteractiveItemUpdate(bool interactiveItemExist){
        this.interactiveItemExist = interactiveItemExist;
        UpdateState();
    }

    public void OnTargetUpdate(bool targetExist){
        this.targetExist = targetExist;
        UpdateState();
    }

    private void UpdateState()
    {
        if (targetExist || interactiveItemExist)
        {
            SetOnTarget();
        }
        else
        {
            SetOffTarget();
        }
    }

    private void UpdateAimTargetState()
    {
        if (targets.Count > 0)
        {
            SetOnTarget();
        }
        else
        {
            SetOffTarget();
        }
    }

    private void SetOnTarget() => aim.SetColor(onTargetColor);
    private void SetOffTarget() => aim.SetColor(offTargetColor);

    public void SetIdleMode() => aim.SetIdleMode();
    public void SetCombatMode() => aim.SetCombatMode();

    public void SetAnimation(AimAnimationsPack aimAnimation, float rate) => animator.SetAnimationData(aimAnimation, rate);
}

