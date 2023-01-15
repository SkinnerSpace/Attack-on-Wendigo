using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropScaler : MonoBehaviour
{
    [SerializeField] private Transform prop;

    [SerializeField] private float minScale = 1f;
    [SerializeField] private float maxScale = 1f;

    private void Awake() => SetScale();

    private void SetScale()
    {
        float randomScale = Rand.Range(minScale, maxScale);
        prop.localScale *= randomScale;
    }
}
