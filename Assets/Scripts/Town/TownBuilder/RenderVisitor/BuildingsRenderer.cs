using UnityEngine;

public class BuildingsRenderer : IFractalVisitor
{
    public void GatherFractalData(FractalData data)
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = data.position;
        plane.transform.localScale = data.size;
    }
}
