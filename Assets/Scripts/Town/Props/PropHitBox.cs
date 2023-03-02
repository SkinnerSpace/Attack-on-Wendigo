using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropHitBox : MonoBehaviour
{
    [SerializeField] private CollapseController collapsible;

    public void PullDown(Vector3 collapseDirection) => collapsible.PullDown(collapseDirection);
}
