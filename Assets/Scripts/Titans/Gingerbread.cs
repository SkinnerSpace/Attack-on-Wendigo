using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Gingerbread : Titan
{  
    private bool active = false;
    [SerializeField] private LegsSynchronizer legsSynchronizer;

    private Vector3 movePosition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            legsSynchronizer.SetActive(true);
        }
    }

    public void Move()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    public override void SetActive(bool active)
    {
        this.active = active;
    }
}
