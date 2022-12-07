using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public static float minArea;
    public static float maxArea;
    public static float averageArea;

    public static float minLength = float.PositiveInfinity;
    public static float maxLength = float.NegativeInfinity;
    public static float averageLength;

    public static float minWidth = float.PositiveInfinity;
    public static float maxWidth = float.NegativeInfinity;
    public static float averageWidth;

    [SerializeField] private float length; public float Length => length;
    [SerializeField] private float width; public float Width => width;
    [SerializeField] private float height; public float Height => height;
}
