using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour, IVisionObserver
{
    private Image image;

    public static Aim Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        image = GetComponent<Image>();
    }

    public void SetOnTarget(bool onTarget) => image.color = onTarget ? Color.red : Color.black;

    public void OnUpdate(VisionTarget target)
    {
        Debug.Log(target.name);
    }
}
