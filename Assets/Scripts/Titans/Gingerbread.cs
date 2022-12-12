using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Gingerbread : Titan
{
    [SerializeField] private float speed = 10f;
    public bool active = false;

    private void Update()
    {
        if (active)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        }
    }
}
