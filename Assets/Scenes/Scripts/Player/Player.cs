using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Look look;
    [SerializeField] private Walk walk;
    [SerializeField] private Jump jump;
    [SerializeField] private Fall fall;

    private void Update()
    {
        look.Execute();
        walk.Execute();
        //jump.Execute();
        fall.Execute();
    }
}
