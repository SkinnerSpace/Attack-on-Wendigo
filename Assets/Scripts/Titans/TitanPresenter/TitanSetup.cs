using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "TitanData", menuName = "ScriptableObjects/TitanSetup", order = 1)]
public class TitanSetup : ScriptableObject
{
    public string titanName;
    public Titans titan;
    public float speed;

    public float stepDistance;
    public float spacing;
    public float stepHeight;
}
