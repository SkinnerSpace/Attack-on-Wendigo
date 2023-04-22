using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Menu
{
    public string name;
    public string title;
    public CanvasGroup container;
    public List<MenuElement> buttons;
}