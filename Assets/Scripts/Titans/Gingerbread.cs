using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Gingerbread : Titan
{
    [SerializeField] private float speed = 10f;

    private void Update()
    {
        /*
        Vector3 direction = (Town.Instance.transform.position - transform.position).normalized;
        Vector3 velocity = direction * speed;
        transform.position += velocity * Time.deltaTime;
        */

        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }
}
