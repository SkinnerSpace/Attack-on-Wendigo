using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TownBuilder : MonoBehaviour
{
    public int length;
    public int width;
    public float areaSize;

    private void Awake()
    {
        FractalBuilder fractalBuilder = new FractalBuilder(length, width, areaSize);
        BuildingsRenderer buildingsRenderer = new BuildingsRenderer();
        fractalBuilder.ComeIn(buildingsRenderer);
    }
}