using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropHitBox : MonoBehaviour, ICollapsible
{
    [SerializeField] private CollapseController collapsible;

    public Vector3 Position => transform.position;
    public void PullDown(Vector3 collapseDirection) => collapsible.PullDown(collapseDirection);
}
