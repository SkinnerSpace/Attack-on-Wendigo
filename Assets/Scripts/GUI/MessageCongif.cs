using UnityEngine;
using System;

[Serializable]
public class MessageCongif
{
    public string message;
    public Color color;
    public float time;

    [Header("Animations")]
    public MessageAppearAnimations appearAnimation;
    public MessageDisappearAnimations disappearAnimation;
}
