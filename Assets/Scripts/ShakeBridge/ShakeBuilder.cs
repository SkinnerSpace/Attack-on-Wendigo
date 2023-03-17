using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShakeBuilder 
{
    public static IShakeBuilder Create() => new ShakeBuilderImp();
}
