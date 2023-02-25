using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UGUIElement : MonoBehaviour
{
    [SerializeField] private MainController player;
    [SerializeField] private KeyBinds.Binds key;
    [SerializeField] private float maxDistance = 3f;

    private Camera cam;
    private Transform target;
    private RectTransform element;
    private TextMeshProUGUI label;

    private void Awake()
    {
        element = GetComponent<RectTransform>();
        label = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        cam = player.Data.Cam;
        label.text = KeyBinds.Instance.Keys[key].ToString();
        player.GetController<PickUpController>().Subscribe(AddTarget, RemoveTarget);
    }

    private void Update()
    {
        if (target != null && IsCloseEnough())
        {
            Vector2 screenPoint = cam.WorldToScreenPoint(target.position);
            element.position = screenPoint;
            label.enabled = true; 
        }
        else
        {
            label.enabled = false;
        }
    }

    private bool IsCloseEnough() => Vector3.Distance(player.transform.position, target.position) < maxDistance;

    private void AddTarget(Transform target) => this.target = target;
    private void RemoveTarget(Transform target) => this.target = null;
}
