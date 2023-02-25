using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Show() => image.enabled = true;
    public void Hid() => image.enabled = false;
    public void SetOnTarget() => image.color = Color.red;
    public void SetOffTarget() => image.color = Color.black;
}
