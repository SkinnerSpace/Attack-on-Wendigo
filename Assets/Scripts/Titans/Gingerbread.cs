using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Gingerbread : Titan
{  
    private bool active = false;
    [SerializeField] private BipedalController bipedalController;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            bipedalController.SetActive(true);
        }

        if (active)
        {
            Move();
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
