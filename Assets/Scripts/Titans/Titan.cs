using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Titan : MonoBehaviour
{
    [SerializeField] public float speed = 10f;

    public abstract void SetActive(bool active);
}
