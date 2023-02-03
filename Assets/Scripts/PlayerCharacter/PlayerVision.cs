using System;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    [SerializeField] private ItemHolder weaponHolder;
    [SerializeField] private PlayerVisionInteractor interactor;
    [SerializeField] private VisionTypeChecker typeChecker;

    private Camera cam;
    public bool hasTarget { get; private set; }
    public RaycastHit Spot => spot;
    private RaycastHit spot;
    private bool suitable;

    public Vector3 point { get; private set; }

    public event Action<bool> notifyOnTarget;

    private void Awake()
    {
        InitializeComponents();
    }

    private void Start() => SubscribeEventListeners();
    private void Update() => Observe();

    private void InitializeComponents()
    {
        cam = GetComponent<Camera>();
    }

    private void SubscribeEventListeners()
    {
        notifyOnTarget += Aim.Instance.SetOnTarget;
    }

    private void Observe()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out spot, Mathf.Infinity, ComplexLayers.Vision);
        hasTarget = spot.transform != null;

        UpdateTargetStatus();
        interactor.InteractIfPossible(spot.transform);

        point = hasTarget ? spot.point : ray.direction * 300f;
    }

    private void UpdateTargetStatus()
    {
        suitable = hasTarget ? IsSuitable(spot.transform) : false;
        notifyOnTarget(suitable);
    }


    private bool IsSuitable(Transform targetTransform)
    {
        Type type = typeChecker.CheckType(targetTransform);
        return type != null;
    }
}
