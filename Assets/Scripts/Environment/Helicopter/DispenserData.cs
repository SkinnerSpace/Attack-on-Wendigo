using UnityEngine;
using UnityEditor;
using System;

public class DispenserData : MonoBehaviour
{
    public float WaitTime => waitTime;
    public float ThrowFoce => throwForce;
    public Transform DropSpace => dropSpace;

    [SerializeField] private float waitTime = 1f;
    [SerializeField] private float throwForce = 100f;
    [SerializeField] private Transform dropSpace;
}
