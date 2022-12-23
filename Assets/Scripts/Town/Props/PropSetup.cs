using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PropData", menuName = "ScriptableObjects/PropSetup", order = 2)]
public class PropSetup : ScriptableObject
{
    public PropTypes type = PropTypes.NONE;

    public List<float> angles = new List<float>();
    public float minScale = 1f;
    public float maxScale = 1f;
}
