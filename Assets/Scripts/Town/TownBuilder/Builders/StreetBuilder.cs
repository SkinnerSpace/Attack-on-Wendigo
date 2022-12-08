using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StreetBuilder : MonoBehaviour, IFractalVisitor
{
    [SerializeField] private int length;
    [SerializeField] private int width;
    [SerializeField] private float areaSize;

    [SerializeField] GameObject street;

    private FractalBuilder fractalBuilder;

    private void Awake()
    {
        CreateFractalBuilder();
        fractalBuilder.ComeIn(this);
    }

    private void CreateFractalBuilder()
    {
        BuilderBootstrap bootstrap = new BuilderBootstrap();
        bootstrap.length = length;
        bootstrap.width = width;
        bootstrap.areaSize = areaSize;

        fractalBuilder = new FractalBuilder(bootstrap);
    }

    public void GatherFractalData(FractalData data)
    {
        Transform newStreet = Instantiate(street, data.position, Quaternion.identity, transform).transform;
        newStreet.localScale = data.size;

    }
}
