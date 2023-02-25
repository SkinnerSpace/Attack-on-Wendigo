using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UGUIElement : MonoBehaviour, IPickUpObserver
{
    [SerializeField] private MainController player;
    [SerializeField] private KeyBinds.Binds key;

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
        player.GetController<PickUpController>().Subscribe(this);
    }

    private void Update()
    {
        if (target != null)
        {
            Vector2 screenPoint = cam.WorldToScreenPoint(target.position);
            element.position = screenPoint;

            if (!label.enabled)
            {
                label.enabled = true;
                label.text = KeyBinds.Instance.Keys[key].ToString();
            }
        }
        else
        {
            label.enabled = false;
        }
    }

    public void OnTargetUpdate(Transform target) => this.target = target;
}
