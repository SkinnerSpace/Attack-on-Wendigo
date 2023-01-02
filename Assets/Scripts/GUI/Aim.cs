using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    private Image image;

    public static Aim Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        image = GetComponent<Image>();
    }

    public void TargetUpdate(bool onTarget)
    {
        image.color = onTarget ? Color.red : Color.black;
    }
}
