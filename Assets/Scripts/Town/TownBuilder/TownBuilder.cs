using UnityEngine;

public class TownBuilder : MonoBehaviour
{
    public int depth;
    public int length;
    public int width;
    public float areaSize;

    private void Awake()
    {
        BuilderBootstrap bootstrap = new BuilderBootstrap();
        bootstrap.depth = depth;
        bootstrap.length = length;
        bootstrap.width = width;
        bootstrap.areaSize = areaSize;

        FractalBuilder fractalBuilder = new FractalBuilder(bootstrap);
        BuildingsRenderer buildingsRenderer = new BuildingsRenderer();
        fractalBuilder.ComeIn(buildingsRenderer);
    }
}