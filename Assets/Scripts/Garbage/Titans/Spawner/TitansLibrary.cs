using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TitansLibrary : MonoBehaviour
{
    [SerializeField] private List<GameObject> titans = new List<GameObject>();

    public GameObject GetTitan(int index)
    {
        return titans[index];
    }
}
