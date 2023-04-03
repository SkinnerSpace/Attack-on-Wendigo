using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class AimPresenter : MonoBehaviour
{
    private const float DISTANCE_UNIT = 16f;
    private const float ROTATION_UNIT = -90f;

    [SerializeField] private RectTransform aim;
    [SerializeField] private List<RectTransform> aimElements;
    [SerializeField] private UGUIElement uGUIElement;

    [Header("Colors")]
    [SerializeField] private Color onTargetColor;
    [SerializeField] private Color offTargetColor;

    [SerializeField] private float defaultDistance;
    [SerializeField] private float defaultRotation;
    [SerializeField] private float defaultScale;

    [SerializeField] private AimAnimationsPack animationData;

    [Range(0f, 1f)]
    [SerializeField] private float completeness;
    [SerializeField] private AimAnimation testAnimation;

    private List<Transform> targets = new List<Transform>();
    private List<Image> aimImages;

    private float distance => defaultDistance + animationDistance;
    private float rotation => defaultRotation + animationRotation;
    private float scale => defaultScale + animationScale;

    private float animationDistance;
    private float animationRotation;
    private float animationScale;

    private void Awake()
    {
        if (uGUIElement != null){
            uGUIElement.Subscribe(OnTargetUpdate);
        }

        FindAimImages();
    }

    private void FindAimImages()
    {
        aimImages = new List<Image>();

        foreach (RectTransform aimElement in aimElements)
        {
            aimImages.Add(aimElement.GetComponent<Image>());
        }
    }

    private void Update()
    {
        UpdateAimAnimation();
        UpdateAimTransform();
    }

    private void UpdateAimAnimation()
    {
        animationDistance = testAnimation.distance.Evaluate(completeness) * DISTANCE_UNIT;
        animationRotation = testAnimation.rotation.Evaluate(completeness) * ROTATION_UNIT;
        animationScale = testAnimation.scale.Evaluate(completeness);
    }

    private void UpdateAimTransform()
    {
        foreach (RectTransform aimElement in aimElements)
        {
            aimElement.localPosition = new Vector3(0f, distance, 0f);
            aim.localEulerAngles = new Vector3(0f, 0f, rotation);
            aim.localScale = new Vector3(scale, scale, 0f);
        }
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

    public void OnTargetUpdate(bool targetExist)
    {
        if (targetExist)
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

    private void SetOnTarget()
    {
        foreach (Image aimImage in aimImages){
            aimImage.color = onTargetColor;
        }
    }

    private void SetOffTarget()
    {
        foreach (Image aimImage in aimImages){
            aimImage.color = offTargetColor;
        }
    }
}
