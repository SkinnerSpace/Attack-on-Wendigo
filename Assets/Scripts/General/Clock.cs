using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Clock : IClock
{
    public float Delta => Time.deltaTime;
}
